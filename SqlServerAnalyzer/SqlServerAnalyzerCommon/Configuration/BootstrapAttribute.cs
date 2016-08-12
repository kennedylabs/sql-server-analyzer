using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SqlServerAnalyzerCommon.Configuration
{
    [AttributeUsage(AttributeTargets.Method)]
    public class BootstrapAttribute : Attribute
    {
        public object[] ParameterValues { get; private set; }

        public BootstrapAttribute(params object[] parameterValues)
        {
            ParameterValues = parameterValues;
        }

        public static bool IsBootstrapMethod(MethodInfo method)
        {
            return method.IsStatic && !method.ContainsGenericParameters && !method.IsConstructor &&
                method.GetCustomAttribute<BootstrapAttribute>() != null;
        }

        public static bool TryInvokeBootstrapMethod(MethodInfo method)
        {
            var success = false;
            try
            {
                method.Invoke(null, method.GetCustomAttribute<BootstrapAttribute>()?
                    .GetRuntimeParameterValues() ?? new object[0]);
                success = true;
            }
            catch { }

            return success;
        }

        public object[] GetRuntimeParameterValues()
        {
            var runtimeValues = new object[ParameterValues.Length];

            for (var index = 0; index < ParameterValues.Length; index++)
            {
                var value = ParameterValues[index];
                object result;
                runtimeValues[index] = value != null && value is LambdaExpression &&
                    TryInvokeLambda((LambdaExpression)value, out result) ? result : value;
            }

            return runtimeValues;
        }

        private bool TryInvokeLambda(LambdaExpression lambdaExpression, out object result)
        {
            result = null;
            var success = false;

            try
            {
                if (lambdaExpression.Parameters.Count == 0 &&
                    lambdaExpression.ReturnType != typeof(void))
                {
                    result = lambdaExpression.Compile().DynamicInvoke();
                    success = true;
                }
            }
            catch { }

            return success;
        }
    }
}

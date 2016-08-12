using SqlServerAnalyzerCommon.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SqlServerAnalyzerCommon.Configuration
{
    public static class SqlAnalyzation
    {
        private static bool _areAssembliesBootstrapped;
        private static ConcurrentDictionary<IEnvironmentConfig, ISqlEnvironement>
            _environments = new ConcurrentDictionary<IEnvironmentConfig, ISqlEnvironement>(
                new EnvironmentConfigComparer());
        private static ConcurrentDictionary<Type, IEnvironmentCreator<IEnvironmentConfig>>
            _environmentCreators = new ConcurrentDictionary<Type,
                IEnvironmentCreator<IEnvironmentConfig>>();

        public static ISqlEnvironement GetOrCreateEnvironment<TEnvironementConfig>(
            TEnvironementConfig config, bool forceCreate = false)
            where TEnvironementConfig : IEnvironmentConfig
        {
            EnsureAssembliesBootstrapped();

            return forceCreate ? _environments.AddOrUpdate(
                config, CreateEnvironment, (c, e) => CreateEnvironment(c)) :
                _environments.GetOrAdd(config, CreateEnvironment);
        }

        public static IEnumerable<ISqlEnvironement> GetAllEnvironements()
        {
            return _environments.Values;
        }

        public static void TryRemoveEnvironemnt<TEnvironmentConfig>(string configName)
            where TEnvironmentConfig : IEnvironmentConfig
        {
            var config = _environments.Keys.FirstOrDefault(
                c => c.GetType() == typeof(TEnvironmentConfig) && c.Name == configName);

            if (config != null) TryRemoveEnvironemnt(config);
        }

        public static void TryRemoveEnvironemnt(IEnvironmentConfig config)
        {
            ISqlEnvironement environment = null;
            if (_environments.TryRemove(config, out environment))
            {
                var disposable = environment as IDisposable;
                if (disposable != null) disposable.Dispose();
            }
        }

        public static void RemoveAllEnvironemnts()
        {
            foreach (var config in _environments.Keys)
                TryRemoveEnvironemnt(config);
        }

        public static void RegisterEnvironmentCreator<TEnvironmentConfig>(
            IEnvironmentCreator<TEnvironmentConfig> environmentCreator, bool forceAdd = false)
            where TEnvironmentConfig : IEnvironmentConfig
        {
            var newCreator = (IEnvironmentCreator<IEnvironmentConfig>)environmentCreator;
            _environmentCreators.AddOrUpdate(typeof(TEnvironmentConfig), newCreator,
                (c, e) => forceAdd ? newCreator : e);
        }

        public static void UnregisterEnvironmentContructor<TEnvironmentConfig>(
            IEnvironmentCreator<TEnvironmentConfig> environmentCreator)
            where TEnvironmentConfig : IEnvironmentConfig
        {
            UnregisterEnvironmentContructor<TEnvironmentConfig>();
        }

        public static void UnregisterEnvironmentContructor<TEnvironmentConfig>()
            where TEnvironmentConfig : IEnvironmentConfig
        {
            IEnvironmentCreator<IEnvironmentConfig> environmentCreator;
            if (_environmentCreators.TryRemove(typeof(TEnvironmentConfig), out environmentCreator))
            {
                {
                    var disposable = environmentCreator as IDisposable;
                    if (disposable != null) disposable.Dispose();
                }
            }
        }

        private static ISqlEnvironement CreateEnvironment<TEnvironmentConfig>(
            TEnvironmentConfig config) where TEnvironmentConfig : IEnvironmentConfig
        {
            IEnvironmentCreator<IEnvironmentConfig> environmentCreator;
            return _environmentCreators.TryGetValue(
                typeof(TEnvironmentConfig), out environmentCreator) ?
                environmentCreator.CreateEnvironement(config) : null;
        }

        private static void EnsureAssembliesBootstrapped()
        {
            lock (typeof(SqlAnalyzation))
            {
                if (!_areAssembliesBootstrapped)
                {
                    foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        foreach (var method in assembly.GetTypes()
                            .SelectMany(t => t.GetMethods())
                            .Where(BootstrapAttribute.IsBootstrapMethod))
                            BootstrapAttribute.TryInvokeBootstrapMethod(method);
                    }

                    _areAssembliesBootstrapped = true;
                }
            }
        }

        private class EnvironmentConfigComparer : EqualityComparer<IEnvironmentConfig>
        {
            public override int GetHashCode(IEnvironmentConfig obj)
            {
                return $"{obj.GetType()}{obj.Name}".GetHashCode();
            }

            public override bool Equals(IEnvironmentConfig x, IEnvironmentConfig y)
            {
                return GetHashCode(x) == GetHashCode(y);
            }
        }
    }
}

using Microsoft.SqlServer.Management.Smo;

namespace SqlServerAnalyzerIntrospection
{
    public class SmoWrapper<TSmoObject> where TSmoObject : SmoObjectBase
    {
        internal TSmoObject SmoObject { get; private set; }

        internal SmoWrapper(TSmoObject smoObject)
        {
            SmoObject = smoObject;
        }
    }
}

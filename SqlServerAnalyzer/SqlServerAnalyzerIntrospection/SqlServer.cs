using Microsoft.SqlServer.Management.Smo;
using SqlServerAnalyzerCommon.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerIntrospection
{
    public class SqlServer : ParentSmoWrapper<Server, Database>, ISqlServer
    {
        internal SqlServer(Server server) : base(server, s => s.Databases)
        {
        }

        public Task<IList<ISqlDatabase>> GetDatabasesAsync()
        {
            return GetChildrenWrappersAsync<ISqlDatabase>(d => new SqlDatabase(d));
        }
    }
}

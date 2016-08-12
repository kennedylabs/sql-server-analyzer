using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using SqlServerAnalyzerCommon.Configuration;
using SqlServerAnalyzerCommon.Implementation;
using SqlServerAnalyzerCommon.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerIntrospection
{
    public class LocalhostSqlEnvironment : SqlEnvironmentBase<LocalhostConfig>
    {
        internal Server Server { get; private set; }

        public LocalhostSqlEnvironment(LocalhostConfig config) : base(config)
        {
            Server = new Server();
        }

        public override Task<IList<ISqlServer>> GetServersAsync()
        {
            var serverList = new List<ISqlServer>();
            if (Server != null) serverList.Add(new SqlServer(Server));
            return Task.FromResult((IList<ISqlServer>)serverList);
        }
    }
}

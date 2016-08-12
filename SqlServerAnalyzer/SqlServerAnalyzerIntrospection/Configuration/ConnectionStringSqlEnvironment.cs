using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using SqlServerAnalyzerCommon.Configuration;
using SqlServerAnalyzerCommon.Implementation;
using SqlServerAnalyzerCommon.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerIntrospection
{
    public class ConnectionStringSqlEnvironment : SqlEnvironmentBase<ConnectionStringConfig>
    {
        internal Server Server { get; private set; }

        public ConnectionStringSqlEnvironment(ConnectionStringConfig config) : base(config)
        {
            var serverConnection = string.IsNullOrWhiteSpace(config.UserName) ?
                new ServerConnection(config.ServerInstance) :
                new ServerConnection(config.ServerInstance, config.UserName, config.Password);

            Server = new Server(serverConnection);
        }

        public override Task<IList<ISqlServer>> GetServersAsync()
        {
            var serverList = new List<ISqlServer>();
            if (Server != null) serverList.Add(new SqlServer(Server));
            return Task.FromResult((IList<ISqlServer>)serverList);
        }
    }
}

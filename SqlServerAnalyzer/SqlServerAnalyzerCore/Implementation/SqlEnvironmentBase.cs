using SqlServerAnalyzerCore.Configuration;
using SqlServerAnalyzerCore.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerCore.Implementation
{
    public abstract class SqlEnvironmentBase<TEnvironmentConfig> : ISqlEnvironement
        where TEnvironmentConfig : IEnvironmentConfig
    {
        public string ConfigName { get; internal set; }

        public SqlEnvironmentBase(TEnvironmentConfig config)
        {
            ConfigName = config.Name;
        }

        public abstract Task<IList<ISqlServer>> GetServers();
    }
}

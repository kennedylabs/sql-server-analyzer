using SqlServerAnalyzerCommon.Configuration;
using SqlServerAnalyzerCommon.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerCommon.Implementation
{
    public abstract class SqlEnvironmentBase : ISqlEnvironement
    {
        public string ConfigName { get; protected set; }

        public abstract Task<IList<ISqlServer>> GetServersAsync();
    }

    public abstract class SqlEnvironmentBase<TEnvironmentConfig> : SqlEnvironmentBase
        where TEnvironmentConfig : IEnvironmentConfig
    {
        public SqlEnvironmentBase(TEnvironmentConfig config)
        {
            ConfigName = config.Name;
        }
    }
}

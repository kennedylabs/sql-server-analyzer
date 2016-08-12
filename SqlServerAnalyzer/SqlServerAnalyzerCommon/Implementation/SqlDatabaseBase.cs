using SqlServerAnalyzerCommon.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerCommon.Implementation
{
    public abstract class SqlDatabaseBase : ISqlDatabase
    {
        public abstract Task<IList<ISqlTable>> GetTablesAsync();
    }
}

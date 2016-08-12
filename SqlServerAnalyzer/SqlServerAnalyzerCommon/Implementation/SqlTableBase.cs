using SqlServerAnalyzerCommon.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerCommon.Implementation
{
    public abstract class SqlTableBase : ISqlTable
    {
        public abstract Task<IList<ISqlColumn>> GetColumnsAsync();
    }
}
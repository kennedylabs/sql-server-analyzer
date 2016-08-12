using SqlServerAnalyzerCore.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerCore.Implementation
{
    public abstract class SqlTableBase : ISqlTable
    {
        public abstract Task<IList<ISqlColumn>> GetColumns();
    }
}
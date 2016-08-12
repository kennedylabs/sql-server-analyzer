using SqlServerAnalyzerCore.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerCore.Implementation
{
    abstract class SqlServerBase : ISqlServer
    {
        public abstract Task<IList<ISqlDatabase>> GetDatabases();
    }
}

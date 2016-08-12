using SqlServerAnalyzerCommon.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerCommon.Implementation
{
    public abstract class SqlServerBase : ISqlServer
    {
        public abstract Task<IList<ISqlDatabase>> GetDatabasesAsync();
    }
}

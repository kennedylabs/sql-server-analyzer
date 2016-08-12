using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerCommon.Interfaces
{
    public interface ISqlServer
    {
        Task<IList<ISqlDatabase>> GetDatabasesAsync();
    }
}

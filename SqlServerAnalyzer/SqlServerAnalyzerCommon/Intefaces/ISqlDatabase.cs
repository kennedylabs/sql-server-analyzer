using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerCommon.Interfaces
{
    public interface ISqlDatabase
    {
        Task<IList<ISqlTable>> GetTablesAsync();
    }
}

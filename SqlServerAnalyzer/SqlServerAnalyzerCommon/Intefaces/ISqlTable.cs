using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerCommon.Interfaces
{
    public interface ISqlTable
    {
        Task<IList<ISqlColumn>> GetColumnsAsync();
    }
}

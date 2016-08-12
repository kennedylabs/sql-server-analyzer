using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerCore.Interfaces
{
    public interface ISqlTable
    {
        Task<IList<ISqlColumn>> GetColumns();
    }
}

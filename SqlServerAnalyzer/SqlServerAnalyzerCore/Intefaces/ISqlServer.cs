
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerCore.Interfaces
{
    public interface ISqlServer
    {
        Task<IList<ISqlDatabase>> GetDatabases();
    }
}

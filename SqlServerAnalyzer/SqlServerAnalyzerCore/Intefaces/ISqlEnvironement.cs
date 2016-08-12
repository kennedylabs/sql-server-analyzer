using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerCore.Interfaces
{
    public interface ISqlEnvironement
    {
        string ConfigName { get; }
        Task<IList<ISqlServer>> GetServers();
    }
}

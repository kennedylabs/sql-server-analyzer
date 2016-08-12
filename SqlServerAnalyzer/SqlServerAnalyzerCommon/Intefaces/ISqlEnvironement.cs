using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerCommon.Interfaces
{
    public interface ISqlEnvironement
    {
        string ConfigName { get; }
        Task<IList<ISqlServer>> GetServersAsync();
    }
}

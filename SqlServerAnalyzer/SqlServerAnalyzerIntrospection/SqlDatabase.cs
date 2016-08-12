using Microsoft.SqlServer.Management.Smo;
using SqlServerAnalyzerCommon.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerIntrospection
{
    public class SqlDatabase : ParentSmoWrapper<Database, Table>, ISqlDatabase
    {
        internal SqlDatabase(Database database) : base(database, d => d.Tables)
        {
        }
        
        public Task<IList<ISqlTable>> GetTablesAsync()
        {
            return GetChildrenWrappersAsync<ISqlTable>(t => new SqlTable(t));
        }
    }
}

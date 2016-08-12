using Microsoft.SqlServer.Management.Smo;
using SqlServerAnalyzerCommon.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlServerAnalyzerIntrospection
{
    public class SqlTable : ParentSmoWrapper<Table, Column>, ISqlTable
    {
        internal SqlTable(Table table) : base(table, d => d.Columns)
        {
        }

        public Task<IList<ISqlColumn>> GetColumnsAsync()
        {
            return GetChildrenWrappersAsync<ISqlColumn>(c => new SqlColumn(c));
        }
    }
}

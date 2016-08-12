using Microsoft.SqlServer.Management.Smo;
using SqlServerAnalyzerCommon.Interfaces;

namespace SqlServerAnalyzerIntrospection
{
    public class SqlColumn : SmoWrapper<Column>, ISqlColumn
    {
        internal SqlColumn(Column column) : base(column)
        {
        }
    }
}

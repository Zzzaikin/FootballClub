using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace QueryPush.Helpers
{
    public static class QueryHelper
    {
        public static List<Dictionary<string, object>> DataTableToDictionaryCollection(DataTable dataTable)
        {
            var columns = dataTable.Columns;
            var rows = dataTable.Rows;

            var columnNamesWithValues = new List<Dictionary<string, object>>();

            for (var i = 0; i < rows.Count; i++)
            {
                var columnNameWithValue = new Dictionary<string, object>();

                for (var j = 0; j < columns.Count; j++)
                {
                    var columnName = columns[j].ColumnName;
                    var columnValue = rows[i][columnName];

                    columnNameWithValue.Add(columnName, columnValue);
                }

                columnNamesWithValues.Add(columnNameWithValue);
            }

            return columnNamesWithValues;
        }

        public static List<string> GetStubs(string paramPrefix, IEnumerable items)
        {
            var stubs = new List<string>();

            var stubIndex = 0;

            foreach (var filter in items)
            {
                var stub = $"@{paramPrefix}{stubIndex}";

                stubs.Add(stub);
                stubIndex++;
            }

            return stubs;
        }
    }
}

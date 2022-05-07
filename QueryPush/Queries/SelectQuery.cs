using Common.Argument;
using MySql.Data.MySqlClient;
using QueryPush.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace QueryPush.Queries
{
    public class SelectQuery : BaseQuery
    {
        public SelectQuery(MySqlConnection connection, QueryModel selectQueryModel) : base(connection, selectQueryModel) { }

        public override DataResult Push()
        {
            SqlCommand ??= new MySqlCommand(SqlExpression, Connection);
            var dataTable = new DataTable();
            var dataAdapter = new MySqlDataAdapter(SqlCommand);

            dataAdapter.Fill(dataTable);

            var columnNamesWithValues = GetDictionaryCollectionFromDataTable(dataTable);
            return new DataResult { Records = columnNamesWithValues };
        }

        public async override Task<DataResult> PushAsync()
        {
            SqlCommand ??= new MySqlCommand(SqlExpression, Connection);
            var dataTable = new DataTable();
            var dataAdapter = new MySqlDataAdapter(SqlCommand);

            await dataAdapter.FillAsync(dataTable);

            var columnNamesWithValues = GetDictionaryCollectionFromDataTable(dataTable);
            return new DataResult { Records = columnNamesWithValues };
        }

        protected internal override void Parse(QueryModel selectQueryModel)
        {
            SqlExpressionStringBuilder.Append("SELECT");

            SetColumns(selectQueryModel);
            SetFrom(selectQueryModel);
            SetJoins(selectQueryModel);
            SetFilters(selectQueryModel);
            SetPage(selectQueryModel.Count, selectQueryModel.From);
        }

        private void SetPage(int count, int from)
        {
            Argument.RecordsCountLessThanMaxValue(count);
            Argument.IntegerNotZero(count, nameof(count));

            SqlExpressionStringBuilder.Append($" LIMIT {count} OFFSET {from}");
        }

        private List<Dictionary<string, object>> GetDictionaryCollectionFromDataTable(DataTable dataTable)
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
    }
}

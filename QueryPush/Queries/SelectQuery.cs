using Common.Argument;
using MySql.Data.MySqlClient;
using QueryPush.Models;
using QueryPush.Models.QueryModels;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace QueryPush.Queries
{
    public class SelectQuery : BaseQuery<SelectQueryModel>
    {
        public SelectQuery(string connectionString, MySqlConnection connection, SelectQueryModel selectQueryModel) 
            : base(connectionString, connection, selectQueryModel) { }

        internal override void Parse(SelectQueryModel selectQueryModel)
        {
            SqlExpression = "SELECT";

            SetColumns(selectQueryModel.Columns);

            var stringBuilder = new StringBuilder(SqlExpression);
            stringBuilder.Append($"FROM {selectQueryModel.EntityName} AS {selectQueryModel.EntityName}");
            SqlExpression = stringBuilder.ToString();

            SetJoins(selectQueryModel.Joins);
            SetFilters(selectQueryModel.Filters);
            SetPage(selectQueryModel.Count, selectQueryModel.From);
        }

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

        private void SetPage(int count, int from)
        {
            Argument.RecordsCountLessThanMaxValue(count);
            Argument.IntegerNotZero(count, nameof(count));

            var stringBuilder = new StringBuilder(SqlExpression);
            stringBuilder.Append($" LIMIT {count} OFFSET {from}");

            SqlExpression = stringBuilder.ToString();
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

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
        public SelectQuery(MySqlConnection connection, SelectQueryModel selectQueryModel) : base(connection, selectQueryModel) { }

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

        protected internal override void Parse(SelectQueryModel selectQueryModel)
        {
            SqlExpression = "SELECT";

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

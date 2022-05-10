using Common.Argument;
using MySql.Data.MySqlClient;
using QueryPush.Helpers;
using QueryPush.Models;
using System.Data;
using System.Threading.Tasks;

namespace QueryPush.Queries
{
    public class SelectQuery : BaseQuery
    {
        public SelectQuery(MySqlConnection connection, QueryModel selectQueryModel) : base(connection, selectQueryModel) { }

        public override DataResult Push()
        {
            var dataTable = new DataTable();
            var dataAdapter = new MySqlDataAdapter(SqlCommand);

            dataAdapter.Fill(dataTable);

            var columnNamesWithValues = QueryHelper.DataTableToDictionaryCollection(dataTable);
            return new DataResult { Records = columnNamesWithValues };
        }

        public async override Task<DataResult> PushAsync()
        {
            var dataTable = new DataTable();
            var dataAdapter = new MySqlDataAdapter(SqlCommand);

            await dataAdapter.FillAsync(dataTable);

            var columnNamesWithValues = QueryHelper.DataTableToDictionaryCollection(dataTable);
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
    }
}

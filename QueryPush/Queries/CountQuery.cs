using MySql.Data.MySqlClient;
using QueryPush.Models;
using QueryPush.Models.QueryModels;
using System.Threading.Tasks;

namespace QueryPush.Queries
{
    public class CountQuery : BaseQuery<BaseQueryModel>
    {
        public CountQuery(MySqlConnection connection, BaseQueryModel queryModel) : base(connection, queryModel)
        { }

        public override DataResult Push()
        {
            SqlCommand ??= new MySqlCommand(SqlExpression, Connection);

            using var reader = SqlCommand.ExecuteReader();
            reader.Read();
            var count = reader.GetValue(0);

            return new DataResult { RecordsCount = count };
        }

        public async override Task<DataResult> PushAsync()
        {
            SqlCommand ??= new MySqlCommand(SqlExpression, Connection);

            using var reader = await SqlCommand.ExecuteReaderAsync();
            await reader.ReadAsync();
            var count = reader.GetValue(0);

            return new DataResult { RecordsCount = count };
        }

        protected internal override void Parse(BaseQueryModel queryModel)
        {
            SqlExpression = "SELECT COUNT(*)";
            SetFrom(queryModel);
            SetJoins(queryModel);
            SetFilters(queryModel);
        }
    }
}

using Common.Argument;
using MySql.Data.MySqlClient;
using QueryPush.Models;

namespace QueryPush.Queries
{
    public class InsertQuery : BaseQuery
    {
        public InsertQuery(MySqlConnection connection, QueryModel queryModel) : base(connection, queryModel) { }

        protected internal override void Parse(QueryModel queryModel)
        {
            SqlExpressionStringBuilder.Append($"INSERT INTO {queryModel.EntityName} (");
            SetColumns(queryModel);
            SetValues(queryModel);

            SqlExpressionStringBuilder.Append(") VALUES (");
        }

        private void SetValues(QueryModel queryModel)
        {
            Argument.NotNull(queryModel, nameof(queryModel));

            var values = queryModel.Values;
            var valuesCount = values.Count;

            Argument.NotNull(values, nameof(values));
            Argument.IntegerNotZero(valuesCount, nameof(valuesCount));

            var stubs = GetStubs(values);
            var index = 0;

            foreach (var value in values)
            {
                SqlExpressionStringBuilder.Append(stubs[index]);

                if (index < valuesCount - 1)
                    SqlExpressionStringBuilder.Append(", ");
            }
        }
    }
}

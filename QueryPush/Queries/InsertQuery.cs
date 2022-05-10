using Common.Argument;
using MySql.Data.MySqlClient;
using QueryPush.Helpers;
using QueryPush.Models;
using System;

namespace QueryPush.Queries
{
    public class InsertQuery : BaseQuery
    {
        public InsertQuery(MySqlConnection connection, QueryModel queryModel) : base(connection, queryModel) { }

        protected internal override void Parse(QueryModel queryModel)
        {
            SqlExpressionStringBuilder.Append($"INSERT INTO {queryModel.EntityName} ( Id, ");
            SetColumns(queryModel);

            SqlExpressionStringBuilder.Append(") VALUES (");
            SetValues(queryModel);
        }

        private void SetValues(QueryModel queryModel)
        {
            Argument.NotNull(queryModel, nameof(queryModel));

            var values = queryModel.Values;
            var valuesCount = values.Count;

            Argument.NotNull(values, nameof(values));
            Argument.IntegerNotZero(valuesCount, nameof(valuesCount));

            SqlExpressionStringBuilder.Append($"\'{Guid.NewGuid()}\', ");

            const string paramPrefix = "valueParam";
            var stubs = QueryHelper.GetStubs(paramPrefix, values);
            var index = 0;

            foreach (var value in values)
            {
                var stub = stubs[index];
                SqlExpressionStringBuilder.Append(stub);

                if (index < valuesCount - 1)
                    SqlExpressionStringBuilder.Append(", ");

                else SqlExpressionStringBuilder.Append(") ");

                Parameters.Add(new MySqlParameter(stub, value));
                index++;
            }
        }
    }
}

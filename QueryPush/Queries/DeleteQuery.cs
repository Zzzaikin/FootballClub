using Common.Argument;
using MySql.Data.MySqlClient;
using QueryPush.Models;
using System;

namespace QueryPush.Queries
{
    public class DeleteQuery : BaseQuery
    {
        public DeleteQuery(MySqlConnection connection, QueryModel queryModel) : base(connection, queryModel)
        { }

        protected internal override void Parse(QueryModel queryModel)
        {
            SqlExpressionStringBuilder.Append($"DELETE FROM {queryModel.EntityName} AS {queryModel.EntityName}");
            SetFilters(queryModel);
        }

        protected internal override void SetFilters(QueryModel queryModel)
        {
            Argument.NotNull(queryModel, nameof(queryModel));
            var filters = queryModel.Filters;

            if ((filters == null) || (filters.Count == 0))
            {
                throw new InvalidOperationException("С тобой всё в порядке? Зачем тебе удалять все строки?");
            }

            base.SetFilters(queryModel);
        }
    }
}

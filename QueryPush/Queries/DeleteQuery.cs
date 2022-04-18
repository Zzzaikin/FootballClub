using Common.Argument;
using MySql.Data.MySqlClient;
using QueryPush.Models.QueryModels;
using System;

namespace QueryPush.Queries
{
    public class DeleteQuery : BaseQuery<BaseQueryModel>
    {
        public DeleteQuery(MySqlConnection connection, BaseQueryModel queryModel) : base(connection, queryModel)
        { }

        protected internal override void Parse(BaseQueryModel queryModel)
        {
            SqlExpression = $"DELETE FROM {queryModel.EntityName} AS {queryModel.EntityName}";
            SetFilters(queryModel);
        }

        protected internal override void SetFilters(BaseQueryModel queryModel)
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

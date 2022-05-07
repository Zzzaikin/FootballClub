using Common.Argument;
using MySql.Data.MySqlClient;
using QueryPush.Models;
using System;

namespace QueryPush.Queries
{
    public class UpdateQuery : BaseQuery
    {
        public UpdateQuery(MySqlConnection connection, QueryModel updateQueryModel) : base(connection, updateQueryModel)
        { }

        protected internal override void Parse(QueryModel updateQueryModel)
        {
            SqlExpressionStringBuilder.Append($"UPDATE {updateQueryModel.EntityName} AS {updateQueryModel.EntityName}");

            SetColumns(updateQueryModel);
            SetFilters(updateQueryModel);
        }

        protected internal override void SetFilters(QueryModel updateQueryModel)
        {
            Argument.NotNull(updateQueryModel, nameof(updateQueryModel));

            var filters = updateQueryModel.Filters;

            if ((filters == null) || (filters.Count == 0))
                throw new InvalidOperationException("С тобой всё в порядке? Зачем тебе обновлять все строки?");

            base.SetFilters(updateQueryModel);
        }

        protected internal override void SetColumns(QueryModel updateQueryModel)
        {
            Argument.NotNull(updateQueryModel, nameof(updateQueryModel));

            var columns = updateQueryModel.Columns;
            var values = updateQueryModel.Values;

            var columnsCount = columns.Count;

            if (columnsCount == 0)
                throw new ArgumentException($"Коллекция {nameof(columns)} оказалась пустой.");

            if (columnsCount != values.Count)
                throw new ArgumentException($"Несоответствие количества колонок и количества их значений.");

            var stubs = GetStubs(columns);
            SqlExpressionStringBuilder.Append(" SET ");

            var index = 0;

            foreach (var column in columns)
            {
                SqlExpressionStringBuilder.Append($"{column} = {stubs[index]}");

                if (index < columnsCount - 1)
                    SqlExpressionStringBuilder.Append(", ");

                index++;
            }

            SetNewSqlCommandWithOldInstanceParameters();
            AddSqlCommandParameters(stubs, values);
        }
    }
}

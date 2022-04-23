using Common.Argument;
using MySql.Data.MySqlClient;
using QueryPush.Models.QueryModels;
using System;
using System.Text;

namespace QueryPush.Queries
{
    public class UpdateQuery : BaseQuery<UpdateQueryModel>
    {
        public UpdateQuery(MySqlConnection connection, UpdateQueryModel updateQueryModel) : base(connection, updateQueryModel)
        { }

        protected internal override void Parse(UpdateQueryModel updateQueryModel)
        {
            SqlExpression = $"UPDATE {updateQueryModel.EntityName} AS {updateQueryModel.EntityName}";

            SetColumns(updateQueryModel);
            SetJoins(updateQueryModel);
            SetFilters(updateQueryModel);
        }

        protected internal override void SetFilters(UpdateQueryModel updateQueryModel)
        {
            Argument.NotNull(updateQueryModel, nameof(updateQueryModel));

            var filters = updateQueryModel.Filters;

            if ((filters == null) || (filters.Count == 0))
                throw new InvalidOperationException("С тобой всё в порядке? Зачем тебе обновлять все строки?");

            base.SetFilters(updateQueryModel);
        }

        protected internal override void SetColumns(UpdateQueryModel updateQueryModel)
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
            var stringBuilder = new StringBuilder(SqlExpression);

            var index = 0;

            foreach (var column in columns)
            {
                stringBuilder.Append($" SET {column.Name} = {stubs[index]}");
                index++;
            }

            var sqlExpression = stringBuilder.ToString();
            SetNewSqlCommandWithOldInstanceParameters(sqlExpression);

            index = 0;

            foreach (var value in values)
            {
                SqlCommand.Parameters.AddWithValue(stubs[index], value);
                index++;
            }
        }
    }
}

using Common.Argument;
using MySql.Data.MySqlClient;
using QueryPush.Enums;
using QueryPush.Models;
using QueryPush.Models.QueryModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueryPush.Queries
{
    public abstract class BaseQuery<TQueryModel> where TQueryModel : BaseQueryModel
    {
        public BaseQuery(MySqlConnection connection, TQueryModel queryModel)
        {
            Connection = connection;
            Parse(queryModel);
        }

        protected string SqlExpression { get; set; }

        protected MySqlConnection Connection { get; set; }

        protected MySqlCommand SqlCommand { get; set; }

        public virtual DataResult Push()
        {
            SqlCommand ??= new MySqlCommand(SqlExpression, Connection);
            var count = SqlCommand.ExecuteNonQuery();

            return new DataResult { AffectedRows = count };
        }

        public async virtual Task<DataResult> PushAsync()
        {
            SqlCommand ??= new MySqlCommand(SqlExpression, Connection);
            var count = await SqlCommand.ExecuteNonQueryAsync();

            return new DataResult { AffectedRows = count };
        }

        protected internal virtual void SetFrom(TQueryModel queryModel)
        {
            var stringBuilder = new StringBuilder(SqlExpression);
            stringBuilder.Append($"FROM {queryModel.EntityName} AS {queryModel.EntityName}");
            SqlExpression = stringBuilder.ToString();
        }

        protected internal virtual void SetColumns(TQueryModel queryModel)
        {
            Argument.NotNull(queryModel, nameof(queryModel));

            var columns = queryModel.Columns;
            var stringBuilder = new StringBuilder(SqlExpression);

            if ((columns == null) || (columns.Count == 0))
            {
                stringBuilder.Append(" * ");
                return;
            }

            var index = 0;
            var lastColumnIndex = columns.Count - 1;

            foreach (var column in columns)
            {
                stringBuilder.Append($" {column.Name}");

                if (index != lastColumnIndex)
                {
                    stringBuilder.Append(", ");
                }

                else
                {
                    stringBuilder.Append(' ');
                }
            }

            SqlExpression = stringBuilder.ToString();
        }

        protected internal virtual void SetJoins(TQueryModel queryModel)
        {
            Argument.NotNull(queryModel, nameof(queryModel));
            var joins = queryModel.Joins;

            if ((joins == null) || (joins.Count == 0))
                return;

            var stringBuilder = new StringBuilder(SqlExpression);

            foreach (var join in joins)
            {
                stringBuilder.Append($" LEFT JOIN {join.Entity.Name} AS {join.Entity.Name}");
                stringBuilder.Append($" ON {join.TargetColumn.Name} = {join.JoinedColumn.Name}");
            }

            SqlExpression = stringBuilder.ToString();
        }

        protected internal virtual void SetFilters(TQueryModel queryModel)
        {
            Argument.NotNull(queryModel, nameof (queryModel));
            var filters = queryModel.Filters;

            if ((filters == null) || (filters.Count == 0))
                return;

            var stringBuilder = new StringBuilder(SqlExpression);
            stringBuilder.Append($" WHERE ");

            var stubs = GetStubs(filters);

            var index = 0;

            foreach (var filter in filters)
            {
                var comparisonType = string.Empty;

                comparisonType = filter.ComparisonType switch
                {
                    ComparisonType.Equal => " = ",
                    ComparisonType.NotEqual => " != ",
                    ComparisonType.NotNull => " NOT NULL ",
                    ComparisonType.IsNull => " IS NULL ",
                    _ => throw new NotImplementedException($"Тип сравнения {filter.ComparisonType} не реализован."),
                };

                var stub = stubs[index];

                var filtersExpression = $" {filter.LeftExpression}{comparisonType}{stub}";
                stringBuilder.Append(filtersExpression);

                index++;
            }

            var sqlExpression = stringBuilder.ToString();
            SetNewSqlCommandWithOldInstanceParameters(sqlExpression);

            index = 0;

            foreach (var filter in filters)
            {
                var stub = stubs[index];
                SqlCommand.Parameters.AddWithValue(stub, filter.RightExpression);

                index++;
            }
        }

        protected internal abstract void Parse(TQueryModel queryModel);

        protected internal List<string> GetStubs(IEnumerable items)
        {
            var stubs = new List<string>();

            var stubIndex = SqlCommand == null ? 0 : SqlCommand.Parameters.Count;

            foreach (var filter in items)
            {
                var stub = $"@{stubIndex}";

                stubs.Add(stub);
                stubIndex++;
            }

            return stubs;
        }

        protected internal void SetNewSqlCommandWithOldInstanceParameters(string sqlExpression)
        {
            SqlExpression = sqlExpression;

            var parameters = SqlCommand?.Parameters;
            SqlCommand = new MySqlCommand(SqlExpression, Connection);

            if ((parameters != null) && (parameters.Count != 0))
            {
                foreach (var parameter in parameters)
                {
                    SqlCommand.Parameters.Add(parameter);
                }
            }
        }
    }
}

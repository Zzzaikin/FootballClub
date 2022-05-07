using Common.Argument;
using MySql.Data.MySqlClient;
using QueryPush.Enums;
using QueryPush.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryPush.Queries
{
    public abstract class BaseQuery
    {
        public BaseQuery(MySqlConnection connection, QueryModel queryModel)
        {
            Connection = connection;
            SqlExpressionStringBuilder = new StringBuilder();

            Parse(queryModel);
        }

        protected internal StringBuilder SqlExpressionStringBuilder { get; set; }

        protected internal string SqlExpression { get => SqlExpressionStringBuilder.ToString(); }

        protected internal MySqlConnection Connection { get; set; }

        protected internal MySqlCommand SqlCommand { get; set; }

        protected internal abstract void Parse(QueryModel queryModel);

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

        protected internal virtual void SetFrom(QueryModel queryModel)
        {
            SqlExpressionStringBuilder.Append($"FROM {queryModel.EntityName} AS {queryModel.EntityName}");
        }

        protected internal virtual void SetColumns(QueryModel queryModel)
        {
            Argument.NotNull(queryModel, nameof(queryModel));

            var columns = queryModel.Columns;
            var columnsCount = columns.Count;

            Argument.NotNull(columns, nameof(columns));
            Argument.IntegerNotZero(columnsCount, nameof(columnsCount));

            var index = 0;
            var lastColumnIndex = columnsCount - 1;

            foreach (var column in columns)
            {
                SqlExpressionStringBuilder.Append($" {column}");

                if (index != lastColumnIndex)
                {
                    SqlExpressionStringBuilder.Append(", ");
                }

                else
                {
                    SqlExpressionStringBuilder.Append(' ');
                }
            }
        }

        protected internal virtual void SetJoins(QueryModel queryModel)
        {
            Argument.NotNull(queryModel, nameof(queryModel));
            var joins = queryModel.Joins;

            if ((joins == null) || (joins.Count == 0))
                return;

            foreach (var join in joins)
            {
                SqlExpressionStringBuilder.Append($" LEFT JOIN {join.EntityName} AS {join.EntityName}");
                SqlExpressionStringBuilder.Append($" ON {join.TargetColumn} = {join.JoinedColumn}");
            }
        }

        protected internal virtual void SetFilters(QueryModel queryModel)
        {
            Argument.NotNull(queryModel, nameof (queryModel));
            var filters = queryModel.Filters;

            if ((filters == null) || (filters.Count == 0))
                return;

            SqlExpressionStringBuilder.Append($" WHERE ");

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
                SqlExpressionStringBuilder.Append(filtersExpression);

                index++;
            }

            SetNewSqlCommandWithOldInstanceParameters();
            AddSqlCommandParameters(stubs, filters.Select(filter => filter.RightExpression).ToList());
        }

        protected internal void AddSqlCommandParameters(List<string> stubs, List<object> values)
        {
            var index = 0;

            foreach (var value in values)
            {
                SqlCommand.Parameters.AddWithValue(stubs[index], value);
                index++;
            }
        }

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

        protected internal void SetNewSqlCommandWithOldInstanceParameters()
        {
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

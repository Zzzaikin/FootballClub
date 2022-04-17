using Common.Argument;
using MySql.Data.MySqlClient;
using QueryPush.Enums;
using QueryPush.Models;
using QueryPush.Models.QueryModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueryPush.Queries
{
    public abstract class BaseQuery<TQueryModel> where TQueryModel : BaseQueryModel
    {
        private string _connectionString;

        public BaseQuery(string connectionString, MySqlConnection connection, TQueryModel queryModel)
        {
            Argument.StringNotNullOrEmpty(connectionString, nameof(connectionString));
            _connectionString = connectionString;
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

        protected void SetColumns(List<Column> columns)
        {
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

        protected void SetJoins(List<Join> joins)
        {
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

        protected void SetFilters(List<Filter> filters)
        {
            if ((filters == null) || (filters.Count == 0))
                return;

            var stringBuilder = new StringBuilder(SqlExpression);
            stringBuilder.Append($" WHERE ");

            var stubs = GetStubs(filters);

            var index = 0;

            foreach (var filter in filters)
            {
                var comparisonType = string.Empty;

                switch (filter.ComparisonType)
                {
                    case ComparisonType.Equal:
                        comparisonType = " = ";
                        break;

                    case ComparisonType.NotEqual:
                        comparisonType = " != ";
                        break;

                    case ComparisonType.NotNull:
                        comparisonType = " NOT NULL ";
                        break;

                    case ComparisonType.IsNull:
                        comparisonType = " IS NULL ";
                        break;

                    default:
                        throw new NotImplementedException($"Тип сравнения {filter.ComparisonType} не реализован.");

                }

                var stub = stubs[index];

                var filtersExpression = $" {filter.LeftExpression}{comparisonType}{stub}";
                stringBuilder.Append(filtersExpression);

                index++;
            }

            SqlExpression = stringBuilder.ToString();
            SqlCommand = new MySqlCommand(SqlExpression, Connection);

            index = 0;

            foreach (var filter in filters)
            {
                var stub = stubs[index];
                SqlCommand.Parameters.AddWithValue(stub, filter.RightExpression);

                index++;
            }
        }

        internal abstract void Parse(TQueryModel queryModel);

        private List<string> GetStubs(List<Filter> filters)
        {
            var stubs = new List<string>();

            var stubIndex = 0;

            foreach (var filter in filters)
            {
                var stub = $"@{stubIndex}";

                stubs.Add(stub);
                stubIndex++;
            }

            return stubs;
        }
    }
}

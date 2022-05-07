using Common.Argument;
using System.Collections.Generic;

namespace QueryPush.Models
{
    public class QueryModel
    {
        private List<Join> _joins;

        private List<string> _columns;

        private string _entityName;

        private int _count;

        public string EntityName 
        { 
            get { return _entityName; } 
            set 
            {
                Argument.ValidateStringByAllPolicies(value, nameof(value));
                _entityName = value; 
            } 
        }

        public List<string> Columns
        {
            get { return _columns;  }
            set
            {
                foreach (var column in value)
                {
                    Argument.ValidateStringByAllPolicies(column, nameof(column));
                }

                _columns = value;
            }

        }

        public List<Filter> Filters { get; set; }

        public List<Join> Joins
        {
            get { return _joins; }
            set
            {
                foreach (var join in value)
                {
                    var joinedColumn = join.JoinedColumn;
                    var targeColumn = join.TargetColumn;
                    var entityName = join.EntityName;

                    Argument.ValidateStringByAllPolicies(joinedColumn, nameof(joinedColumn));
                    Argument.ValidateStringByAllPolicies(targeColumn, nameof(targeColumn));
                    Argument.ValidateStringByAllPolicies(entityName, nameof(entityName));
                }

                _joins = value;
            }

        }

        public int From { get; set; } = 0;

        public int Count
        {
            get { return _count; }
            set
            {
                Argument.RecordsCountLessThanMaxValue(value);
                _count = value;
            }
        }

        public List<object> Values { get; set; }
    }
}

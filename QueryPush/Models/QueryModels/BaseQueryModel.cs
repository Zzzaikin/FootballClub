using Common.Argument;
using System.Collections.Generic;

namespace QueryPush.Models.QueryModels
{
    public class BaseQueryModel
    {
        private string _entityName;

        public string EntityName 
        { 
            get { return _entityName; } 
            set 
            {
                Argument.ValidateStringByAllPolicies(value, nameof(value));
                _entityName = value; 
            } 
        }

        public List<Column> Columns { get; set; }

        public List<Filter> Filters { get; set; }

        public List<Join> Joins { get; set; }
    }
}

using Common.Argument;
using QueryPush.Enums;

namespace QueryPush.Models
{
    public class Filter
    {
        private string _leftExpression;

        private ComparisonType _comparisonType;

        public string LeftExpression 
        { 
            get { return _leftExpression; } 
            set 
            { 
                Argument.ValidateStringByAllPolicies(value, nameof(value)); 
                _leftExpression = value;
            } 
        }

        public ComparisonType ComparisonType
        {
            get { return _comparisonType; }
            set 
            {
                Argument.IntegerMoreThanZero((int)value, nameof(value));
                _comparisonType = value; 
            }
        }

        public object RightExpression { get; set; }
    }
}

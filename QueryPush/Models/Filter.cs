using Common.Argument;
using QueryPush.Enums;

namespace QueryPush.Models
{
    public class Filter
    {
        private string _leftExpression;

        private string _rightExpression;

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
                Argument.IntegerNotZero((int)value, nameof(value));
                _comparisonType = value; 
            }
        }

        public string RightExpression
        {
            get { return _rightExpression; }
            set
            {
                Argument.ValidateStringByAllPolicies(value, nameof(value));
                _rightExpression = value;
            }
        }
    }
}

using Common.Argument;

namespace QueryPush.Models
{
    public class Entity
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                Argument.ValidateStringByAllPolicies(value, nameof(value));
                _name = value;
            }
        }
    }
}

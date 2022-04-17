using Common.Argument;

namespace QueryPush.Models.QueryModels
{
    public class SelectQueryModel : BaseQueryModel
    {
        private int _count;

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
    }
}

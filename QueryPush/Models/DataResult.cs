using System.Collections.Generic;

namespace QueryPush.Models
{
    public class DataResult
    {
        public int AffectedRows { get; set; }

        public List<Dictionary<string, object>> Records { get; set; }

        public object RecordsCount { get; set; }
    }
}

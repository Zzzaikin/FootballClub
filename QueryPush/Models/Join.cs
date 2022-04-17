namespace QueryPush.Models
{
    public class Join
    {
        public Entity Entity { get; set; }

        public Column TargetColumn { get; set; }

        public Column JoinedColumn { get; set; }
    }
}

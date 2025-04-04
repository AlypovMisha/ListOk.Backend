namespace ListOk.Core.Models
{
    public class Board
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<Column> Columns { get; set; } = new List<Column>();
    }
}

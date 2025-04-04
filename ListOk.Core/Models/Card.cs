namespace ListOk.Core.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ColumnId { get; set; }
        public Column Column { get; set; }
    }
}

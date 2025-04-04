namespace ListOk.Core.DTOs
{
    public class CardDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ColumnId { get; set; }
    }
}

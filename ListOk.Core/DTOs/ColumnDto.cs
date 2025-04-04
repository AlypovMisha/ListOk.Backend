namespace ListOk.Core.DTOs
{
    public class ColumnDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid BoardId { get; set; }
        public List<CardDto> Cards { get; set; } = new List<CardDto>();
    }
}

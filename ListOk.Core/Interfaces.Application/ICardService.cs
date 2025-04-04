using ListOk.Core.DTOs;

namespace ListOk.Core.Interfaces
{
    public interface ICardService
    {
        Task<IEnumerable<CardDto>> GetCardsByColumnIdAsync(Guid columnId);
        Task<CardDto> GetCardByIdAsync(Guid id);
        Task<CardDto> CreateCardAsync(string title, string description, Guid columnId);
        Task<CardDto> UpdateCardAsync(Guid id, string title, string description);
        Task MoveCardAsync(Guid id, Guid destinationColumnId);
        Task DeleteCardAsync(Guid id);
    }
}

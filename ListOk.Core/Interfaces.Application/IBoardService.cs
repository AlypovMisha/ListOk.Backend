using ListOk.Application.DTOs;

namespace ListOk.Core.Interfaces
{
    public interface IBoardService
    {
        Task<IEnumerable<BoardDto>> GetBoardsAsync();
        Task<BoardDto> CreateBoardAsync(string boardName);
        Task<BoardDto> GetBoardByIdAsync(Guid id);
        Task<BoardDto> UpdateBoardAsync(Guid id, string boardName);
        Task DeleteBoardAsync(Guid id);    
    }
}

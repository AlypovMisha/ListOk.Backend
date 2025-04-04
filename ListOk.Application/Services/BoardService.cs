using ListOk.Application.DTOs;
using ListOk.Core.DTOs;
using ListOk.Core.Interfaces;
using ListOk.Core.Interfaces.Infrastucture;
using ListOk.Core.Models;

namespace ListOk.Application.Services
{
    public class BoardService(IBoardRepository _boardRepository) : IBoardService
    {
        public async Task<IEnumerable<BoardDto>> GetBoardsAsync()
        {
            var boards = await _boardRepository.GetBoardsAsync();
            return boards.Select(MapToDto);
        }

        public async Task<BoardDto> GetBoardByIdAsync(Guid id)
        {
            var board = await _boardRepository.GetBoardByIdAsync(id);
            return board != null ? MapToDto(board) : null;
        }

        public async Task<BoardDto> CreateBoardAsync(string boardTitle)
        {
            var board = new Board { Id = Guid.NewGuid(), Title = boardTitle };
            await _boardRepository.AddBoardAsync(board);
            return MapToDto(board);
        }

        public async Task<BoardDto> UpdateBoardAsync(Guid id, string boardTitle)
        {
            var board = await _boardRepository.GetBoardByIdAsync(id);
            if (board == null) throw new KeyNotFoundException("Board not found");

            board.Title = boardTitle;
            await _boardRepository.UpdateBoardAsync(board);
            return MapToDto(board);
        }

        public async Task DeleteBoardAsync(Guid id)
        {
            await _boardRepository.DeleteBoardAsync(id);
        }

        private BoardDto MapToDto(Board board)
        {
            return new BoardDto
            {
                Id = board.Id,
                Title = board.Title,
                Columns = board.Columns?.Select(c => new ColumnDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    BoardId = board.Id,
                    Cards = c.Cards?.Select(card => new CardDto
                    {
                        Id = card.Id,
                        Title = card.Title,
                        Description = card.Description
                    }).ToList()
                }).ToList()
            };
        }
    }
}
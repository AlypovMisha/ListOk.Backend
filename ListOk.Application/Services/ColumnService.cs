using ListOk.Core.DTOs;
using ListOk.Core.Interfaces;
using ListOk.Core.Interfaces.Infrastucture;
using ListOk.Core.Models;

namespace ListOk.Application.Services
{
    public class ColumnService(IColumnRepository _columnRepository) : IColumnService
    {
        public async Task<IEnumerable<ColumnDto>> GetColumnsByBoardIdAsync(Guid boardId)
        {
            var columns = await _columnRepository.GetColumnsByBoardIdAsync(boardId);
            return columns.Select(c => new ColumnDto
            {
                Id = c.Id,
                Title = c.Title,
                BoardId = c.BoardId,
                Cards = c.Cards.Select(card => new CardDto
                {
                    Id = card.Id,
                    Title = card.Title,
                    Description = card.Description
                }).ToList()
            });
        }

        public async Task<ColumnDto> CreateColumnAsync(string columnName, Guid boardId)
        {
            var column = new Column
            {
                Id = Guid.NewGuid(),
                Title = columnName,
                BoardId = boardId
            };
            await _columnRepository.AddColumnAsync(column);

            return new ColumnDto
            {
                Id = column.Id,
                Title = column.Title,
                BoardId = column.BoardId
            };
        }

        public async Task<ColumnDto> UpdateColumnAsync(Guid id, string columnName)
        {
            var column = await _columnRepository.GetColumnByIdAsync(id);
            if (column == null)
                throw new KeyNotFoundException("Column not found");

            column.Title = columnName;
            await _columnRepository.UpdateColumnAsync(column);

            return new ColumnDto
            {
                Id = column.Id,
                Title = column.Title,
                BoardId = column.BoardId
            };
        }

        public async Task DeleteColumnAsync(Guid id)
        {
            await _columnRepository.DeleteColumnAsync(id);
        }
    }
}

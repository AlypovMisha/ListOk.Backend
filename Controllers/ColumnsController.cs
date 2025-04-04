using ListOk.Core.DTOs;
using ListOk.Core.Interfaces;
using ListOk.Presentation.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ListOk.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColumnsController(IColumnService _columnService) : ControllerBase
    {
        // Получить все колонки доски
        [HttpGet("board/{boardId:guid}")]
        public async Task<ActionResult<IEnumerable<ColumnDto>>> GetColumns(Guid boardId)
        {
            var columns = await _columnService.GetColumnsByBoardIdAsync(boardId);
            return Ok(columns);
        }

        // Создать новую колонку
        [HttpPost]
        public async Task<ActionResult<ColumnDto>> CreateColumn([FromBody] CreateColumnRequest request)
        {
            var column = await _columnService.CreateColumnAsync(request.Title, request.BoardId);
            return Ok(column);
        }

        // Обновить колонку
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateColumn(Guid id, [FromBody] UpdateColumnRequest request)
        {
            try
            {
                await _columnService.UpdateColumnAsync(id, request.Title);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // Удалить колонку
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteColumn(Guid id)
        {
            await _columnService.DeleteColumnAsync(id);
            return NoContent();
        }
    }
}

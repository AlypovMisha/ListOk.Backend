using ListOk.Application.DTOs;
using ListOk.Core.Interfaces;
using ListOk.Presentation.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ListOk.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardsController(IBoardService _boardService) : ControllerBase
    {
        [HttpGet(Name = "GetAllBoards")]
        public async Task<ActionResult<IEnumerable<BoardDto>>> GetBoards()
        {
            var boards = await _boardService.GetBoardsAsync();
            return Ok(boards);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BoardDto>> GetBoard(Guid id)
        {
            var board = await _boardService.GetBoardByIdAsync(id);
            if (board == null)
            {
                return NotFound();
            }
            return Ok(board);
        }

        [HttpPost]
        public async Task<ActionResult<BoardDto>> CreateBoard([FromBody] CreateBoardRequest request)
        {
            var board = await _boardService.CreateBoardAsync(request.Title);
            return CreatedAtAction(nameof(GetBoard), new { id = board.Id }, board);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBoard(Guid id, [FromBody] UpdateBoardRequest request)
        {
            var board = await _boardService.UpdateBoardAsync(id, request.Title);
            if (board == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(Guid id)
        {
            await _boardService.DeleteBoardAsync(id);
            return NoContent();
        }
    }
}


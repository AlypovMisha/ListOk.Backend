using ListOk.Core.DTOs;
using ListOk.Core.Interfaces;
using ListOk.Presentation.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ListOk.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController(ICardService _cardService) : ControllerBase
    {
        // Получить все карточки колонки
        [HttpGet("column/{columnId:guid}")]
        public async Task<ActionResult<IEnumerable<CardDto>>> GetCards(Guid columnId)
        {
            var cards = await _cardService.GetCardsByColumnIdAsync(columnId);
            return Ok(cards);
        }

        // Получить карточку по Id
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CardDto>> GetCard(Guid id)
        {
            var card = await _cardService.GetCardByIdAsync(id);
            if (card == null)
                return NotFound();
            return Ok(card);
        }

        // Создать новую карточку
        [HttpPost]
        public async Task<ActionResult<CardDto>> CreateCard([FromBody] CreateCardRequest request)
        {
            var card = await _cardService.CreateCardAsync(request.Title, request.Description, request.ColumnId);
            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }

        // Обновить карточку
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCard(Guid id, [FromBody] UpdateCardRequest request)
        {
            try
            {
                await _cardService.UpdateCardAsync(id, request.Title, request.Description);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // Переместить карточку в другую колонку
        [HttpPut("{id:guid}/move")]
        public async Task<IActionResult> MoveCard(Guid id, [FromBody] MoveCardRequest request)
        {
            try
            {
                await _cardService.MoveCardAsync(id, request.DestinationColumnId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // Удалить карточку
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            await _cardService.DeleteCardAsync(id);
            return NoContent();
        }
    }
}
using ListOk.Core.DTOs;
using ListOk.Core.Interfaces;
using ListOk.Core.Interfaces.Infrastucture;
using ListOk.Core.Models;

namespace ListOk.Application.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<IEnumerable<CardDto>> GetCardsByColumnIdAsync(Guid columnId)
        {
            var cards = await _cardRepository.GetCardsByColumnIdAsync(columnId);
            return cards.Select(c => new CardDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ColumnId = c.ColumnId
            });
        }

        public async Task<CardDto> GetCardByIdAsync(Guid id)
        {
            var card = await _cardRepository.GetCardByIdAsync(id);
            if (card == null) return null;
            return new CardDto
            {
                Id = card.Id,
                Title = card.Title,
                Description = card.Description,
                ColumnId = card.ColumnId
            };
        }

        public async Task<CardDto> CreateCardAsync(string title, string description, Guid columnId)
        {
            var card = new Card
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description,
                ColumnId = columnId
            };

            await _cardRepository.AddCardAsync(card);

            return new CardDto
            {
                Id = card.Id,
                Title = card.Title,
                Description = card.Description,
                ColumnId = card.ColumnId
            };
        }

        public async Task<CardDto> UpdateCardAsync(Guid id, string title, string description)
        {
            var card = await _cardRepository.GetCardByIdAsync(id);
            if (card == null)
                throw new KeyNotFoundException("Card not found");

            card.Title = title;
            card.Description = description;
            await _cardRepository.UpdateCardAsync(card);

            return new CardDto
            {
                Id = card.Id,
                Title = card.Title,
                Description = card.Description,
                ColumnId = card.ColumnId
            };
        }

        public async Task MoveCardAsync(Guid id, Guid destinationColumnId)
        {
            var card = await _cardRepository.GetCardByIdAsync(id);
            if (card == null)
                throw new KeyNotFoundException("Card not found");

            card.ColumnId = destinationColumnId;
            await _cardRepository.UpdateCardAsync(card);
        }

        public async Task DeleteCardAsync(Guid id)
        {
            await _cardRepository.DeleteCardAsync(id);
        }
    }
}

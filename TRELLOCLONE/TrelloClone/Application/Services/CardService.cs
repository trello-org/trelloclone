using Application.Services.Interfaces;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloClone.Models;


namespace Application.Services
{
	public class CardService
	{
		private readonly ICardRepository _cardRepository;

		public CardService(ICardRepository cardRepository)
		{
			_cardRepository = cardRepository;
		}

		public Task AssignCardAsync(long cardId, long userId)
		{
			return _cardRepository.AssignCardAsync(cardId, userId);
		}

		public Task RemoveAssigneeFromCardAsync(long cardId, long userId)
		{
			return _cardRepository.RemoveAssigneeFromCardAsync(cardId, userId);
		}

		public Task<Card> GetByIdAsync(long id)
		{
			return _cardRepository.GetByIdAsync(id);
		}

		public Task<List<Card>> GetAllAsync()
		{
			return _cardRepository.GetAllAsync();
		}

		public Task<List<Card>> FindAsync(Expression<Func<Card, bool>> expression)
		{
			return _cardRepository.FindAsync(expression);
		}

		public Task AddAsync(Card entity)
		{
			return _cardRepository.AddAsync(entity);
		}

		public Task RemoveAsync(long id)
		{
			return _cardRepository.RemoveAsync(id);
		}

		public Task RemoveRangeAsync(IEnumerable<Card> entities)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(Card entity)
		{
			return _cardRepository.UpdateAsync(entity);
		}
	}
}

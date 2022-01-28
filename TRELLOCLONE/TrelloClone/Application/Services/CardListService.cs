
using Application.Services.Interfaces;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace TrelloClone.Services
{
	public class CardListService 
	{ 
		private readonly ICardListRepository _cardListRepository;

		public CardListService(ICardListRepository cardListRepository)
		{
			_cardListRepository = cardListRepository;
		}


		public Task<CardList> GetByIdAsync(long id)
		{
			return _cardListRepository.GetByIdAsync(id);
		}

		public Task<List<CardList>> GetAllAsync()
		{
			return _cardListRepository.GetAllAsync();
		}

		public Task<List<CardList>> FindAsync(Expression<Func<CardList, bool>> expression)
		{
			return _cardListRepository.FindAsync(expression);
		}

		public Task AddAsync(CardList entity)
		{
			return _cardListRepository.AddAsync(entity);
		}

		public Task RemoveAsync(long id)
		{
			return _cardListRepository.RemoveAsync(id);
		}

		public Task RemoveRangeAsync(IEnumerable<CardList> entities)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(CardList entity)
		{
			return _cardListRepository.UpdateAsync(entity);
		}
	}
}

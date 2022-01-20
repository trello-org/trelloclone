using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace TrelloClone.Services
{
	public class CardListService
	{
		private readonly ApplicationContext _dbContext;

		public CardListService(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}

		internal void UpdateCardList(CardList cardList)
		{
			_dbContext.CardLists.Update(cardList);
			_dbContext.SaveChanges();
		}

		internal void DeleteCardList(Guid id)
		{
			CardList toBeRemoved = _dbContext.CardLists.
				Where(cl => cl.Id == id)
				.Include(cl => cl.Cards)
				.ThenInclude(c => c.Labels)
				.FirstOrDefault();

			foreach(Card c in toBeRemoved.Cards)
			{
				_dbContext.Labels.RemoveRange(c.Labels);
			}
			_dbContext.Cards.RemoveRange(toBeRemoved.Cards);
			_dbContext.CardLists.Remove(toBeRemoved);
			_dbContext.SaveChanges();
		}

		internal void CreateCardListFoBoard(Guid boardId, CardList cardList)
		{
			// not sure if I need to add it both to parent class and childCollection
			_dbContext.Boards.Single(b => b.Id == boardId).CardLists.Append(cardList);
			_dbContext.SaveChanges();
		}

		
	}
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;
using TrelloClone.Models.Dtos;

namespace TrelloClone.Services
{
	public class CardService
	{
		private readonly ApplicationContext _dbContext;

		public CardService(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}

		internal void CreateCardForCardList(Guid cardListId, Card card)
		{
			_dbContext.CardLists.Single(cl => cl.Id == cardListId).Cards.Append(card);
			_dbContext.SaveChanges();
		}

		internal void DeleteCard(Guid id)
		{
			Card toBeRemoved = _dbContext.Cards.Where(c => c.Id == id).Include(c => c.Labels).FirstOrDefault();
			_dbContext.Labels.RemoveRange(toBeRemoved.Labels);
			_dbContext.Cards.Remove(toBeRemoved);
			_dbContext.SaveChanges();
		}

		internal void AddLabelToCard(CardLabelDto cardLabelDto)
		{
			Card card = _dbContext.Cards.Where(c => c.Id == cardLabelDto.CardId).Include(c => c.Labels).FirstOrDefault();
			Label label = _dbContext.Labels.Single(l => l.Id == cardLabelDto.LabelId);

			card.Labels.Append(label);
			_dbContext.Cards.Update(card);
			_dbContext.SaveChanges();
		}

		internal void DeleteCardNoCascade(Guid id)
		{
			_dbContext.Cards.Remove(_dbContext.Cards.Single(c => c.Id == id));
			_dbContext.SaveChanges();
		}
	}
}

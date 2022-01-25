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

		internal void CreateCardForCardList(long cardListId, Card card)
		{
			
		}

		internal void DeleteCard(Guid id)
		{
			throw new NotImplementedException();
		}

		internal void AddLabelToCard(CardLabelDto cardLabelDto)
		{
			throw new NotImplementedException();
		}

		internal void DeleteCardNoCascade(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}

using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrelloClone;
using TrelloClone.Models;
using TrelloClone.Models.Dtos;

namespace Repository
{
	public class CardListRepository : ICardListRepository
	{
		private readonly ApplicationContext _dbContext;
		private readonly string _connectionString;

		public CardListRepository(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
			_connectionString = Environment.GetEnvironmentVariable("adoString");
		}
		public void Add(CardList entity)
		{
			_dbContext.CardLists.Add(entity);
			_dbContext.SaveChanges();
		}

		public void AddRange(IEnumerable<CardList> entities)
		{
			_dbContext.CardLists.AddRange(entities);
			_dbContext.SaveChanges();
		}

		

		public IEnumerable<CardList> Find(Expression<Func<CardList, bool>> expression)
		{
			return _dbContext.CardLists.Where(expression);
		}

		public IEnumerable<CardList> GetAll()
		{
			return _dbContext.CardLists;
		}

		public CardList GetById(long id)
		{
			return _dbContext.CardLists.SingleOrDefault(cl => cl.Id == id);
		}

		public void Remove(long id)
		{
			CardList toBeRemoved = _dbContext.CardLists
				.Where(u => u.Id == id)
				.Include(c => c.Cards)
				.ThenInclude(l => l.Labels)
				.FirstOrDefault();
				
				foreach (Card c in toBeRemoved.Cards)
				{
					_dbContext.Labels.RemoveRange(c.Labels);
				}
				
			_dbContext.Cards.RemoveRange(toBeRemoved.Cards);
			_dbContext.CardLists.Remove(toBeRemoved);
			_dbContext.SaveChanges();
		}

		

		public void RemoveRange(IEnumerable<CardList> entities)
		{
			throw new NotImplementedException();
		}

		public void Update(CardList entity)
		{
			_dbContext.CardLists.Update(entity);
			_dbContext.SaveChanges();
		}
	}
}

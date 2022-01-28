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
		
		public async Task AddAsync(CardList entity)
		{
			await _dbContext.CardLists.AddAsync(entity);
			await _dbContext.SaveChangesAsync();
		}

		public Task<List<CardList>> FindAsync(Expression<Func<CardList, bool>> expression)
		{
			return _dbContext.CardLists.Where(expression).ToListAsync();
		}

		public Task<List<CardList>> GetAllAsync()
		{
			return _dbContext.CardLists.ToListAsync();
		}

		public  Task<CardList> GetByIdAsync(long id)
		{
			return _dbContext.CardLists.SingleOrDefaultAsync(cl => cl.Id == id);
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

		public async Task RemoveAsync(long id)
		{
			CardList toBeRemoved = await _dbContext.CardLists
				.Where(u => u.Id == id)
				.Include(c => c.Cards)
				.ThenInclude(l => l.Labels)
				.FirstOrDefaultAsync();

			foreach (Card c in toBeRemoved.Cards)
			{
				_dbContext.Labels.RemoveRange(c.Labels);
			}

			_dbContext.Cards.RemoveRange(toBeRemoved.Cards);
			_dbContext.CardLists.Remove(toBeRemoved);
			await _dbContext.SaveChangesAsync();
		}

		public Task RemoveRangeAsync(IEnumerable<CardList> entities)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(CardList entity)
		{
			_dbContext.CardLists.Update(entity);
			return _dbContext.SaveChangesAsync();
		}
	}
}

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

		public async Task AddAsync(CardList entity)
		{
			await _dbContext.CardLists.AddAsync(entity);
			await _dbContext.SaveChangesAsync();
		}

		public void AddRange(IEnumerable<CardList> entities)
		{
			_dbContext.CardLists.AddRange(entities);
			_dbContext.SaveChanges();
		}

		public async Task AddRangeAsync(IEnumerable<CardList> entities)
		{
			await _dbContext.CardLists.AddRangeAsync(entities);
			await _dbContext.SaveChangesAsync();
		}

		public IEnumerable<CardList> Find(Expression<Func<CardList, bool>> expression)
		{
			return _dbContext.CardLists.Where(expression);
		}

		public async Task<IEnumerable<CardList>> FindAsync(Expression<Func<CardList, bool>> expression)
		{
			return await _dbContext.CardLists.Where(expression).ToListAsync();
		}

		public IEnumerable<CardList> GetAll()
		{
			return _dbContext.CardLists;
		}

		public async Task<IEnumerable<CardList>> GetAllAsync()
		{
			return await _dbContext.CardLists.ToListAsync();
		}

		public CardList GetById(long id)
		{
			return _dbContext.CardLists.SingleOrDefault(cl => cl.Id == id);
		}

		public async Task<CardList> GetByIdAsync(long id)
		{
			return await _dbContext.CardLists.SingleOrDefaultAsync(cl => cl.Id == id);
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

		public void RemoveRange(IEnumerable<CardList> entities)
		{
			throw new NotImplementedException();
		}

		public Task RemoveRangeAsync(IEnumerable<CardList> entities)
		{
			throw new NotImplementedException();
		}

		public void Update(CardList entity)
		{
			_dbContext.CardLists.Update(entity);
			_dbContext.SaveChanges();
		}

		public async Task UpdateAsync(CardList entity)
		{
			_dbContext.CardLists.Update(entity);
			await _dbContext.SaveChangesAsync();
		}
	}
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrelloClone;
using TrelloClone.Models;

namespace Repository.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationContext _dbContext;
		private readonly string _connectionString;

		public UserRepository(ApplicationContext dbContext, string connectionString)
		{
			_dbContext = dbContext;
			_connectionString = connectionString;
		}
		public void Add(User entity)
		{
			_dbContext.Users.Add(entity);
			_dbContext.SaveChanges();
		}

		public async Task AddAsync(User entity)
		{
			await _dbContext.AddAsync(entity);
			await _dbContext.SaveChangesAsync();
		}

		public void AddRange(IEnumerable<User> entities)
		{
			_dbContext.Users.AddRange(entities);
			_dbContext.SaveChanges();
		}

		public Task AddRangeAsync(IEnumerable<User> entities)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<User> Find(Expression<Func<User, bool>> expression)
		{
			return _dbContext.Users.Where(expression);
		}

		public async Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> expression)
		{
			return await _dbContext.Users.Where(expression).ToListAsync();
		}

		public IEnumerable<User> GetAll()
		{
			return _dbContext.Users;
		}

		public async Task<IEnumerable<User>> GetAllAsync()
		{
			return await _dbContext.Users.ToListAsync();
		}

		public User GetById(long id)
		{
			return _dbContext.Users.SingleOrDefault(u => u.Id == id);
		}

		public Task<User> GetByIdAsync(long id)
		{
			return _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
		}

		public void Remove(long id)
		{
			User toBeRemoved = _dbContext.Users
				.Where(u => u.Id == id)
				.Include(b => b.Boards)
				.ThenInclude(cl => cl.CardLists)
				.ThenInclude(c => c.Cards)
				.ThenInclude(l => l.Labels)
				.FirstOrDefault();
			foreach (Board b in toBeRemoved.Boards)
			{
				foreach (CardList cl in b.CardLists)
				{
					foreach (Card c in cl.Cards)
					{
						_dbContext.Labels.RemoveRange(c.Labels);
					}
					_dbContext.Cards.RemoveRange(cl.Cards);
				}
				_dbContext.CardLists.RemoveRange(b.CardLists);
			}
			_dbContext.Boards.RemoveRange(toBeRemoved.Boards);
			_dbContext.Users.Remove(toBeRemoved);
			_dbContext.SaveChanges();

		}

		public async Task RemoveAsync(long id)
		{
			User toBeRemoved = await _dbContext.Users
				.Where(u => u.Id == id)
				.Include(b => b.Boards)
				.ThenInclude(cl => cl.CardLists)
				.ThenInclude(c => c.Cards)
				.ThenInclude(l => l.Labels)
				.FirstOrDefaultAsync();
			foreach (Board b in toBeRemoved.Boards)
			{
				foreach (CardList cl in b.CardLists)
				{
					foreach (Card c in cl.Cards)
					{
						_dbContext.Labels.RemoveRange(c.Labels);
					}
					_dbContext.Cards.RemoveRange(cl.Cards);
				}
				_dbContext.CardLists.RemoveRange(b.CardLists);
			}
			_dbContext.Boards.RemoveRange(toBeRemoved.Boards);
			_dbContext.Users.Remove(toBeRemoved);
			await _dbContext.SaveChangesAsync();
		}

		public void RemoveRange(IEnumerable<User> entities)
		{
			throw new NotImplementedException();
		}

		public Task RemoveRangeAsync(IEnumerable<User> entities)
		{
			throw new NotImplementedException();
		}

		public void Update(User entity)
		{
			_dbContext.Users.Update(entity);
			_dbContext.SaveChanges();
		}

		public async Task UpdateAsync(User entity)
		{
			_dbContext.Users.Update(entity);
			await _dbContext.SaveChangesAsync();
		}
	}
}

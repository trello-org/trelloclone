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

		public UserRepository(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
			_connectionString = Environment.GetEnvironmentVariable("adoString");
		}
		public void Add(User entity)
		{
			_dbContext.Users.Add(entity);
			_dbContext.SaveChanges();
		}

		public void AddRange(IEnumerable<User> entities)
		{
			_dbContext.Users.AddRange(entities);
			_dbContext.SaveChanges();
		}

		public IEnumerable<User> Find(Expression<Func<User, bool>> expression)
		{
			return _dbContext.Users.Where(expression);
		}

		public IEnumerable<User> GetAll()
		{
			return _dbContext.Users;
		}

		public User GetById(int id)
		{
			return _dbContext.Users.SingleOrDefault(u => u.Id == id);
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

		public void RemoveRange(IEnumerable<User> entities)
		{
			throw new NotImplementedException();
		}
	}
}

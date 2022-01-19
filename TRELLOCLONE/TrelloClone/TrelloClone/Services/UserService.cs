using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace TrelloClone.Services
{
	public class UserService
	{
		private readonly ApplicationContext _dbContext;

		public UserService(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}

		internal IEnumerable<User> GetAllUsers()
		{
			return _dbContext.Users;
		}

		internal void CreateUser(User user)
		{
			_dbContext.Add(user);
			_dbContext.SaveChanges();
		}

		internal void EditUser(Guid id, User user)
		{
			_dbContext.Update(user);
			_dbContext.SaveChanges();
		}

		internal void DeleteUser(Guid id)
		{
			_dbContext.Remove(_dbContext.Users.Single(u => u.Id == id));
			_dbContext.SaveChanges();
		}

		internal User GetUserById(Guid id)
		{
			return _dbContext.Users.Single(u => u.Id == id);
		}
	}
}

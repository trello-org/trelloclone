using Microsoft.EntityFrameworkCore;
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
			return _dbContext.Users.Include(u => u.Boards);
		}

		internal void CreateUser(User user)
		{
			if (user.Boards == null) user.Boards = new List<Board>();
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
			User toBeRemoved = _dbContext.Users
				.Where(u => u.Id == id)
				.Include(b => b.Boards)
				.ThenInclude(cl => cl.CardLists)
				.ThenInclude(c => c.Cards)
				.ThenInclude(l => l.Labels)
				.FirstOrDefault();
			foreach(Board b in toBeRemoved.Boards)
			{
				foreach(CardList cl in b.CardLists)
				{
					foreach(Card c in cl.Cards)
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

		internal User GetUserById(Guid id)
		{
			return _dbContext.Users.Single(u => u.Id == id);
		}

		internal IEnumerable<Board> GetAllBoardsForUser(Guid userId)
		{
			User user = _dbContext.Users
				.Where(u => u.Id == userId)
				.Include(u => u.Boards)
				.FirstOrDefault();

			return user.Boards;

		}
	}
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace TrelloClone.Services
{
	public class BoardService
	{
		private readonly ApplicationContext _dbContext;

		public BoardService(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}

		internal IEnumerable<Board> GetAllBoardsForUser()
		{
			return _dbContext.Boards;
		}

		internal Board GetBoardById(Guid id)
		{
			return _dbContext.Boards.Where(b => b.Id == id).Include(b => b.CardLists).ThenInclude(cl => cl.Cards).ThenInclude(c => c.Labels).FirstOrDefault();
		}

		internal IEnumerable<Board> GetAllBoardsForUser(Guid userId)
		{
			User user = _dbContext.Users
				.Where(u => u.Id == userId)
				.Include(u => u.Boards)
				.FirstOrDefault();

			return user.Boards;
			
		}

		internal void CreateBoard(Guid userId, Board board)
		{
			_dbContext.Users.Single(u => u.Id == userId).Boards.Append(board);
			_dbContext.Boards.Add(board);
			_dbContext.SaveChanges();
		}

		internal void EditBoard(Board board)
		{
			_dbContext.Update(board);
			_dbContext.SaveChanges();
		}

		internal void DeleteBoard(Guid id)
		{
			Board toBeRemoved = _dbContext.Boards
				.Where(b => b.Id == id)
				.Include(b => b.CardLists)
				.ThenInclude(cl => cl.Cards)
				.ThenInclude(c => c.Labels)
				.FirstOrDefault();

			foreach (CardList cl in toBeRemoved.CardLists)
			{
				foreach (Card c in cl.Cards)
				{
					_dbContext.Labels.RemoveRange(c.Labels);
				}
				_dbContext.Cards.RemoveRange(cl.Cards);
			}
			_dbContext.CardLists.RemoveRange(toBeRemoved.CardLists);
			_dbContext.Boards.Remove(toBeRemoved);
			_dbContext.SaveChanges();

		}
	}
}

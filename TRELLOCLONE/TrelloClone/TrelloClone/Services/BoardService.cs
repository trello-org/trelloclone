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
			return _dbContext.Boards.Single(b => b.Id == id);
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
			_dbContext.Remove(_dbContext.Boards.Single(b => b.Id == id));
			_dbContext.SaveChanges();

		}
	}
}

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

namespace Repository
{
	public class BoardRepository : IBoardRepository
	{
		private readonly ApplicationContext _dbContext;
		private readonly string _connectionString;

		public BoardRepository(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
			_connectionString = Environment.GetEnvironmentVariable("adoString");
		}
		public void Add(Board entity)
		{
			_dbContext.Add(entity);
			_dbContext.SaveChanges();
		}

		public void AddRange(IEnumerable<Board> entities)
		{
			_dbContext.Boards.AddRange(entities);
			_dbContext.SaveChanges();
		}

		public void EditBoardVisibility(Board board)
		{
			_dbContext.Boards.Update(board);
			_dbContext.SaveChanges();
		}

		public IEnumerable<Board> Find(Expression<Func<Board, bool>> expression)
		{
			return _dbContext.Boards.Where(expression);
		}

		public IEnumerable<Board> GetAll()
		{
			return _dbContext.Boards;
		}

		public IEnumerable<Board> GetAllBoardsForUser(long id)
		{
			return _dbContext.Boards.Where(b => b.UserId == id);
		}

		public Board GetById(long id)
		{
			return _dbContext.Boards.SingleOrDefault(b => b.Id == id);
		}

		public void Remove(long id)
		{
			Board toBeRemoved = _dbContext.Boards
				.Where(u => u.Id == id)
				.Include(cl => cl.CardLists)
				.ThenInclude(c => c.Cards)
				.ThenInclude(l => l.Labels)
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

		public void RemoveRange(IEnumerable<Board> entities)
		{
			throw new NotImplementedException();
		}

		public void Update(Board entity)
		{
			_dbContext.Boards.Update(entity);
			_dbContext.SaveChanges();
		}
	}
}

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

		public async Task AddAsync(Board entity)
		{
			await _dbContext.AddAsync(entity);
			await _dbContext.SaveChangesAsync();
		}

		public Task EditBoardVisibilityAsync(Board board)
		{
			_dbContext.Boards.Update(board);
			return _dbContext.SaveChangesAsync();
		}

		public Task<List<Board>> FindAsync(Expression<Func<Board, bool>> expression)
		{
			return _dbContext.Boards.Where(expression).ToListAsync();
		}
		public Task<List<Board>> GetAllAsync()
		{
			return _dbContext.Boards.ToListAsync();
		}

		public Task<List<Board>> GetAllBoardsForUserAsync(long id)
		{
			return _dbContext.Boards.Where(b => b.UserId == id).ToListAsync();
		}

		public Task<Board> GetByIdAsync(long id)
		{
			return _dbContext.Boards.SingleOrDefaultAsync(b => b.Id == id);
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

		public async Task RemoveAsync(long id)
		{
			Board toBeRemoved = await _dbContext.Boards
				.Where(u => u.Id == id)
				.Include(cl => cl.CardLists)
				.ThenInclude(c => c.Cards)
				.ThenInclude(l => l.Labels)
				.FirstOrDefaultAsync();

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
			await _dbContext.SaveChangesAsync();
		}

		public Task RemoveRangeAsync(IEnumerable<Board> entities)
		{
			throw new NotImplementedException();
		}


		public Task UpdateAsync(Board entity)
		{
			_dbContext.Boards.Update(entity);
			return _dbContext.SaveChangesAsync();
		}
	}
	
}

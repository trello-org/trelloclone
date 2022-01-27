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

		public async Task AddAsync(Board entity)
		{
			await _dbContext.AddAsync(entity);
			await _dbContext.SaveChangesAsync();
		}

		public void AddRange(IEnumerable<Board> entities)
		{
			_dbContext.Boards.AddRange(entities);
			_dbContext.SaveChanges();
		}

		public async Task AddRangeAsync(IEnumerable<Board> entities)
		{
			await _dbContext.Boards.AddRangeAsync(entities);
			await _dbContext.SaveChangesAsync();
		}

		public void EditBoardVisibility(Board board)
		{
			_dbContext.Boards.Update(board);
			_dbContext.SaveChanges();
		}

		public async Task EditBoardVisibilityAsync(Board board)
		{
			_dbContext.Boards.Update(board);
			await _dbContext.SaveChangesAsync();
		}

		public IEnumerable<Board> Find(Expression<Func<Board, bool>> expression)
		{
			return _dbContext.Boards.Where(expression);
		}

		public async Task<IEnumerable<Board>> FindAsync(Expression<Func<Board, bool>> expression)
		{
			return await _dbContext.Boards.Where(expression).ToListAsync();
		}

		public IEnumerable<Board> GetAll()
		{
			return _dbContext.Boards;
		}

		public async Task<IEnumerable<Board>> GetAllAsync()
		{
			return await _dbContext.Boards.ToListAsync();
		}

		public IEnumerable<Board> GetAllBoardsForUser(long id)
		{
			return _dbContext.Boards.Where(b => b.UserId == id);
		}

		public async Task<IEnumerable<Board>> GetAllBoardsForUserAsync(long id)
		{
			return await _dbContext.Boards.Where(b => b.UserId == id).ToListAsync();
		}

		public Board GetById(long id)
		{
			return _dbContext.Boards.SingleOrDefault(b => b.Id == id);
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

		public void RemoveRange(IEnumerable<Board> entities)
		{
			throw new NotImplementedException();
		}

		public Task RemoveRangeAsync(IEnumerable<Board> entities)
		{
			throw new NotImplementedException();
		}

		public void Update(Board entity)
		{
			_dbContext.Boards.Update(entity);
			_dbContext.SaveChanges();
		}

		public async Task UpdateAsync(Board entity)
		{
			_dbContext.Boards.Update(entity);
			await _dbContext.SaveChangesAsync();
		}
	}
	
}

using Application.Services.Interfaces;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace TrelloClone.Services
{
	public class BoardService
	{
		private readonly IBoardRepository _boardRepository;

		public BoardService(IBoardRepository boardRepository)
		{
			_boardRepository = boardRepository;
		}

		public Task<List<Board>> GetAllBoardsForUserAsync(long id)
		{
			return _boardRepository.GetAllBoardsForUserAsync(id);
		}

		public Task EditBoardVisibilityAsync(Board board)
		{
			return _boardRepository.EditBoardVisibilityAsync(board);
		}

		public Task<Board> GetByIdAsync(long id)
		{
			return _boardRepository.GetByIdAsync(id);
		}

		public Task<List<Board>> GetAllAsync()
		{
			return _boardRepository.GetAllAsync();
		}

		public Task<List<Board>> FindAsync(Expression<Func<Board, bool>> expression)
		{
			return _boardRepository.FindAsync(expression);
		}

		public Task AddAsync(Board entity)
		{
			return _boardRepository.AddAsync(entity);
		}

		public Task RemoveAsync(long id)
		{
			return _boardRepository.RemoveAsync(id);
		}

		public Task RemoveRangeAsync(IEnumerable<Board> entities)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(Board entity)
		{
			return _boardRepository.UpdateAsync(entity);
		}
	}
}

using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace Repository
{
	public class BoardRepository : IBoardRepository
	{
		public void Add(Board entity)
		{
			throw new NotImplementedException();
		}

		public void AddRange(IEnumerable<Board> entities)
		{
			throw new NotImplementedException();
		}

		public void EditBoardVisibility(Board board)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Board> Find(Expression<Func<Board, bool>> expression)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Board> GetAll()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Board> GetAllBoardsForUser(long id)
		{
			throw new NotImplementedException();
		}

		public Board GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Remove(Board entity)
		{
			throw new NotImplementedException();
		}

		public void Remove(long id)
		{
			throw new NotImplementedException();
		}

		public void RemoveRange(IEnumerable<Board> entities)
		{
			throw new NotImplementedException();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace Repository.Repository
{
	public interface IBoardRepository : IRepository<Board>
	{
		IEnumerable<Board> GetAllBoardsForUser(long id);
		void EditBoardVisibility(Board board);

		Task<IEnumerable<Board>> GetAllBoardsForUserAsync(long id);
		Task EditBoardVisibilityAsync(Board board);
	}
}

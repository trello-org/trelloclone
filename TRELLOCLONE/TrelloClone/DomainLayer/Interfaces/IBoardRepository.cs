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
		Task<List<Board>> GetAllBoardsForUserAsync(long id);
		Task EditBoardVisibilityAsync(Board board);
	}
}

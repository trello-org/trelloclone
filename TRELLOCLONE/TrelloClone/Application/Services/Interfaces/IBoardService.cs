using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace Application.Services.Interfaces
{
	public interface IBoardService : IService<Board>
	{
		IEnumerable<Board> GetAllBoardsForUser(long id);
		void EditBoardVisibility(Board board);

		Task<IEnumerable<Board>> GetAllBoardsForUserAsync(long id);
		Task EditBoardVisibilityAsync (Board board);
	}
}

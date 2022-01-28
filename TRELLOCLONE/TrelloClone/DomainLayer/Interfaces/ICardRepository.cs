using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace Repository.Repository
{
	public interface ICardRepository : IRepository<Card>
	{
		Task AssignCardAsync(long cardId, long userId);
		Task RemoveAssigneeFromCardAsync(long cardId, long userId);
	}
}

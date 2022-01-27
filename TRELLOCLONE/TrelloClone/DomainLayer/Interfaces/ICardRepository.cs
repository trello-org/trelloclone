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
		void AssignCard(long cardId, long userId);
		void RemoveAssigneeFromCard(long cardId, long userId);
	}
}

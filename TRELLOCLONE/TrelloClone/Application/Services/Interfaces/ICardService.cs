using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Models;
using TrelloClone.Models.Dtos;

namespace Application.Services.Interfaces
{
	public interface ICardService : IService<Card>
	{
		void AssignCard(long cardId, long userId);
		void RemoveAssigneeFromCard(long cardId, long userId);

		Task AssignCardAsync(long cardId, long userId);
		Task RemoveAssigneeFromCardAsync(long cardId, long userId);
	}
}

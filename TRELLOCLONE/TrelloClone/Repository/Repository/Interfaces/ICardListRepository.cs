using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Models;
using TrelloClone.Models.Dtos;

namespace Repository.Repository
{
	public interface ICardListRepository : IRepository<CardList>
	{
		void AssignCard(CardAsigneeDto cardAsigneeDto);
		void RemoveAssigneeFromCard(CardAsigneeDto cardAsigneeDto);

	}
}

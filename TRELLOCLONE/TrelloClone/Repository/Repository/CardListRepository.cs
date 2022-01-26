using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Models;
using TrelloClone.Models.Dtos;

namespace Repository
{
	public class CardListRepository : ICardListRepository
	{
		public void Add(CardList entity)
		{
			throw new NotImplementedException();
		}

		public void AddRange(IEnumerable<CardList> entities)
		{
			throw new NotImplementedException();
		}

		public void AssignCard(CardAsigneeDto cardAsigneeDto)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CardList> Find(Expression<Func<CardList, bool>> expression)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CardList> GetAll()
		{
			throw new NotImplementedException();
		}

		public CardList GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Remove(CardList entity)
		{
			throw new NotImplementedException();
		}

		public void Remove(long id)
		{
			throw new NotImplementedException();
		}

		public void RemoveAssigneeFromCard(CardAsigneeDto cardAsigneeDto)
		{
			throw new NotImplementedException();
		}

		public void RemoveRange(IEnumerable<CardList> entities)
		{
			throw new NotImplementedException();
		}
	}
}

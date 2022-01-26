using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace Repository.Repository
{
	public class CardRepository : ICardRepository
	{
		public void Add(Card entity)
		{
			throw new NotImplementedException();
		}

		public void AddRange(IEnumerable<Card> entities)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Card> Find(Expression<Func<Card, bool>> expression)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Card> GetAll()
		{
			throw new NotImplementedException();
		}

		public Card GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Remove(Card entity)
		{
			throw new NotImplementedException();
		}

		public void Remove(long id)
		{
			throw new NotImplementedException();
		}

		public void RemoveRange(IEnumerable<Card> entities)
		{
			throw new NotImplementedException();
		}
	}
}

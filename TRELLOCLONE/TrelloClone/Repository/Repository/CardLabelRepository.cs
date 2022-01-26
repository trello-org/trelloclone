using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace Repository.Repository
{
	public class CardLabelRepository : ICardLabelRepository
	{
		public void Add(Label entity)
		{
			throw new NotImplementedException();
		}

		public void AddRange(IEnumerable<Label> entities)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Label> Find(Expression<Func<Label, bool>> expression)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Label> GetAll()
		{
			throw new NotImplementedException();
		}

		public Label GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Remove(Label entity)
		{
			throw new NotImplementedException();
		}

		public void Remove(long id)
		{
			throw new NotImplementedException();
		}

		public void RemoveRange(IEnumerable<Label> entities)
		{
			throw new NotImplementedException();
		}
	}
}

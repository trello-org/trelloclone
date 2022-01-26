using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrelloClone;
using TrelloClone.Models;

namespace Repository.Repository
{
	public class CardLabelRepository : ICardLabelRepository
	{
		private readonly ApplicationContext _dbContext;
		private readonly string _connectionString;

		public CardLabelRepository(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
			_connectionString = Environment.GetEnvironmentVariable("adoString");
		}
		public void Add(Label entity)
		{
			_dbContext.Labels.Add(entity);
			_dbContext.SaveChanges();
		}

		public void AddRange(IEnumerable<Label> entities)
		{
			_dbContext.Labels.AddRange(entities);
			_dbContext.SaveChanges();
		}

		public IEnumerable<Label> Find(Expression<Func<Label, bool>> expression)
		{
			return _dbContext.Labels.Where(expression);
		}

		public IEnumerable<Label> GetAll()
		{
			return _dbContext.Labels;
		}

		public Label GetById(long id)
		{
			return _dbContext.Labels.Single(l => l.Id == id);
		}

		public void Remove(Label entity)
		{
			_dbContext.Labels.Remove(entity);
			_dbContext.SaveChanges();
		}

		public void Remove(long id)
		{
			_dbContext.Labels.Remove(_dbContext.Labels.Single(l => l.Id == id));
			_dbContext.SaveChanges();
		}

		public void RemoveRange(IEnumerable<Label> entities)
		{
			throw new NotImplementedException();
		}

		public void Update(Label entity)
		{
			_dbContext.Labels.Update(entity);
			_dbContext.SaveChanges();
		}
	}
}

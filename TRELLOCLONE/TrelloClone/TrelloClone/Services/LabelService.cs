using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace TrelloClone.Services
{
	public class LabelService
	{
		private readonly ApplicationContext _dbContext;

		public LabelService(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}

		internal void CreateLabel(Label label)
		{
			_dbContext.Labels.Add(label);
			_dbContext.SaveChanges();
		}

		internal void DeleteLabel(long id)
		{
			_dbContext.Labels.Remove(_dbContext.Labels.Single(l => l.Id == id));
			_dbContext.SaveChanges();
		}
	}
}

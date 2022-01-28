using Microsoft.EntityFrameworkCore;
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

		public async Task AddAsync(Label entity)
		{
			await _dbContext.Labels.AddAsync(entity);
			await _dbContext.SaveChangesAsync();
		}

		public Task<List<Label>> FindAsync(Expression<Func<Label, bool>> expression)
		{
			return _dbContext.Labels.Where(expression).ToListAsync();
		}

		public Task<List<Label>> GetAllAsync()
		{
			return _dbContext.Labels.ToListAsync();
		}

		public Task<Label> GetByIdAsync(long id)
		{
			return _dbContext.Labels.SingleOrDefaultAsync(l => l.Id == id);
		}

		public async Task RemoveAsync(long id)
		{
			_dbContext.Labels.Remove(await _dbContext.Labels.SingleOrDefaultAsync(l => l.Id == id));
			await _dbContext.SaveChangesAsync();
		}

		public Task RemoveRangeAsync(IEnumerable<Label> entities)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(Label entity)
		{
			_dbContext.Labels.Update(entity);
			return _dbContext.SaveChangesAsync();
		}
	}
}

using Microsoft.EntityFrameworkCore;
using Repository.EntityTypeConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace Repository.Repository
{
	public class CardLabelRepository : ICardLabelRepository
	{
		private readonly ApplicationContext _dbContext;
		private readonly ConnectionStrings _connectionStrings;

		public CardLabelRepository(ApplicationContext dbContext, ConnectionStrings connectionStrings)
		{
			_dbContext = dbContext;
			_connectionStrings = connectionStrings;
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

using Application.Services.Interfaces;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace Application.Services
{
	public class LabelService
	{
		private ICardLabelRepository _cardLabelRepository;
		
		public LabelService(ICardLabelRepository cardLabelRepository)
		{
			_cardLabelRepository = cardLabelRepository;
		}

		public Task AddAsync(Label entity)
		{
			return _cardLabelRepository.AddAsync(entity);
		}


		public Task<List<Label>> FindAsync(Expression<Func<Label, bool>> expression)
		{
			return _cardLabelRepository.FindAsync(expression);
		}

		public Task<List<Label>> GetAllAsync()
		{
			return _cardLabelRepository.GetAllAsync();
		}

		public Task<Label> GetByIdAsync(long id)
		{
			return _cardLabelRepository.GetByIdAsync(id);
		}

		public Task RemoveAsync(long id)
		{
			return _cardLabelRepository.RemoveAsync(id);
		}

		public void RemoveRange(IEnumerable<Label> entities)
		{
			throw new NotImplementedException();
		}

		public Task RemoveRangeAsync(IEnumerable<Label> entities)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(Label entity)
		{
			return _cardLabelRepository.UpdateAsync(entity);
		}
	}
}

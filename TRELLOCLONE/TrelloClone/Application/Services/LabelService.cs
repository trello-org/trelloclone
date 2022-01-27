using Application.Services.Interfaces;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace TrelloClone.Services
{
	public class LabelService : ICardLabelService
	{
		private ICardLabelRepository _cardLabelRepository;
		
		public LabelService(ICardLabelRepository cardLabelRepository)
		{
			_cardLabelRepository = cardLabelRepository;
		}
		public void Add(Label label)
		{
			/*using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("INSERT INTO public.labels(name, color_hex, card_id) VALUES (@name, @color, @id); ", connection);

				NpgsqlParameter nameParam = new NpgsqlParameter("@name", NpgsqlTypes.NpgsqlDbType.Varchar, label.Name.Length);
				nameParam.Value = label.Name;

				NpgsqlParameter colorParam = new NpgsqlParameter("@desc", NpgsqlTypes.NpgsqlDbType.Varchar, label.ColorHex.Length);
				colorParam.Value = label.ColorHex;

				NpgsqlParameter cardIdParam = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Bigint);
				cardIdParam.Value = label.CardId;

				cm.Parameters.Add(nameParam);
				cm.Parameters.Add(colorParam);
				cm.Parameters.Add(cardIdParam);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}*/
			_cardLabelRepository.Add(label);
		}

		public async Task AddAsync(Label entity)
		{
			await _cardLabelRepository.AddAsync(entity);
		}

		public void AddRange(IEnumerable<Label> entities)
		{
			throw new NotImplementedException();
		}

		public async Task AddRangeAsync(IEnumerable<Label> entities)
		{
			await _cardLabelRepository.AddRangeAsync(entities);
		}

		public IEnumerable<Label> Find(Expression<Func<Label, bool>> expression)
		{
			return _cardLabelRepository.Find(expression);
		}

		public async Task<IEnumerable<Label>> FindAsync(Expression<Func<Label, bool>> expression)
		{
			return await _cardLabelRepository.FindAsync(expression);
		}

		public IEnumerable<Label> GetAll()
		{
			return _cardLabelRepository.GetAll();
		}

		public async Task<IEnumerable<Label>> GetAllAsync()
		{
			return await _cardLabelRepository.GetAllAsync();
		}

		public Label GetById(long id)
		{
			return _cardLabelRepository.GetById(id);
		}

		public async Task<Label> GetByIdAsync(long id)
		{
			return await _cardLabelRepository.GetByIdAsync(id);
		}

		public void Remove(long id)
		{
			/*using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("delete from labels where id = @id; ", connection);


				NpgsqlParameter idParam = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Bigint);
				idParam.Value = id;

				cm.Parameters.Add(idParam);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}*/
			_cardLabelRepository.Remove(id);
		}

		public async Task RemoveAsync(long id)
		{
			await _cardLabelRepository.RemoveAsync(id);
		}

		public void RemoveRange(IEnumerable<Label> entities)
		{
			throw new NotImplementedException();
		}

		public Task RemoveRangeAsync(IEnumerable<Label> entities)
		{
			throw new NotImplementedException();
		}

		public  void Update(Label entity)
		{
			_cardLabelRepository.Update(entity);
		}

		public async Task UpdateAsync(Label entity)
		{
			await _cardLabelRepository.UpdateAsync(entity);
		}
	}
}

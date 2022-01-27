using Application.Services.Interfaces;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

		public void AddRange(IEnumerable<Label> entities)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Label> Find(Expression<Func<Label, bool>> expression)
		{
			return _cardLabelRepository.Find(expression);
		}

		public IEnumerable<Label> GetAll()
		{
			return _cardLabelRepository.GetAll();
		}

		public Label GetById(long id)
		{
			return _cardLabelRepository.GetById(id);
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

		public void RemoveRange(IEnumerable<Label> entities)
		{
			throw new NotImplementedException();
		}

		public void Update(Label entity)
		{
			_cardLabelRepository.Update(entity);
		}
	}
}

using Microsoft.Extensions.Configuration;
using Npgsql;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace TrelloClone.Services
{
	public class LabelService
	{
		private ICardLabelRepository _cardLabelRepository;
		
		public LabelService(ICardLabelRepository cardLabelRepository)
		{
			_cardLabelRepository = cardLabelRepository;
		}
		internal void CreateLabel(Label label)
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

		internal void DeleteLabel(long id)
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
	}
}

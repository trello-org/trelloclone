using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace TrelloClone.Services
{
	public class CardListService
	{
		private readonly ApplicationContext _dbContext;
		private readonly IConfiguration _configuration;
		private readonly string _connectionString;

		public CardListService(IConfiguration configuration)
		{
			_configuration = configuration;
			_connectionString = _configuration["PostgreSql:ConnectionStringADO"];
		}

		internal void UpdateCardList(CardList cardList)
		{
			using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("UPDATE cardlists SET name = @name WHERE id = @id; ", connection);

				NpgsqlParameter nameParam = new NpgsqlParameter("@name", NpgsqlTypes.NpgsqlDbType.Varchar, cardList.Name.Length);
				nameParam.Value = cardList.Name;

				NpgsqlParameter idParam = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Bigint);
				idParam.Value = cardList.Id;

				cm.Parameters.Add(nameParam);
				cm.Parameters.Add(idParam);

				cm.Prepare();
				cm.ExecuteNonQuery();
			};
		}

		internal void DeleteCardList(long id)
		{
			using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("delete from cardlists where id = @id;", connection);

				NpgsqlParameter idParam = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Bigint);
				idParam.Value = id;

				cm.Parameters.Add(id);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}
		}

		internal void CreateCardListForBoard(long boardId, CardList cardList)
		{
			using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("INSERT INTO public.cardlists(name, board_id) VALUES (@name, @board_id);", connection);

				NpgsqlParameter nameParam = new NpgsqlParameter("@name", NpgsqlTypes.NpgsqlDbType.Varchar, cardList.Name.Length);
				nameParam.Value = cardList.Name;

				NpgsqlParameter idParam = new NpgsqlParameter("@url", NpgsqlTypes.NpgsqlDbType.Bigint);
				idParam.Value = boardId;

				cm.Parameters.Add(nameParam);
				cm.Parameters.Add(idParam);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}
		}

		
	}
}

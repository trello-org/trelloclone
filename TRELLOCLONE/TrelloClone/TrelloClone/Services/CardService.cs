using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;
using TrelloClone.Models.Dtos;

namespace TrelloClone.Services
{
	public class CardService
	{
		private readonly ApplicationContext _dbContext;
		private readonly IConfiguration _configuration;
		private readonly string _connectionString;

		public CardService(IConfiguration configuration)
		{
			_configuration = configuration;
			_connectionString = _configuration["PostgreSql:ConnectionStringADO"];
		}

		internal void CreateCardForCardList(long cardListId, Card card)
		{
			using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("INSERT INTO public.cards(name, description, card_list_id) VALUES(@name, @desc, @id); ", connection);

				NpgsqlParameter nameParam = new NpgsqlParameter("@name", NpgsqlTypes.NpgsqlDbType.Varchar, card.Name.Length);
				nameParam.Value = card.Name;

				NpgsqlParameter descParam = new NpgsqlParameter("@desc", NpgsqlTypes.NpgsqlDbType.Varchar, card.Description.Length);
				descParam.Value = card.Description;

				NpgsqlParameter cardListIdParam = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Bigint);
				cardListIdParam.Value = card.CardListId;

				cm.Parameters.Add(nameParam);
				cm.Parameters.Add(descParam);
				cm.Parameters.Add(cardListIdParam);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}
		}

		internal void DeleteCard(long id)
		{
			using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("delete from cards where id = @id;", connection);

				cm.Parameters.AddWithValue("@id", id);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}
		}

		internal void AddLabelToCard(CardLabelDto cardLabelDto)
		{
			throw new NotImplementedException();
		}

		internal void DeleteCardNoCascade(Guid id)
		{
			throw new NotImplementedException();
		}

		internal void AssignCard(CardAsigneeDto cardAsigneeDto)
		{
			using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("INSERT INTO public.asignees(user_id, card_id) VALUES(@user_id, @card_id); ", connection);

				NpgsqlParameter userIdParam = new NpgsqlParameter("@user_id", NpgsqlTypes.NpgsqlDbType.Bigint);
				userIdParam.Value = cardAsigneeDto.UserId;

				NpgsqlParameter cardIdParam = new NpgsqlParameter("@card_id", NpgsqlTypes.NpgsqlDbType.Bigint);
				cardIdParam.Value = cardAsigneeDto.CardId;

				cm.Parameters.Add(userIdParam);
				cm.Parameters.Add(cardIdParam);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}
		}

		internal void RemoveAssigneeFromCard(CardAsigneeDto cardAsigneeDto)
		{
			using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("delete from asignees where user_id = @user_id and card_id = @card_id;", connection);

				NpgsqlParameter userIdParam = new NpgsqlParameter("@user_id", NpgsqlTypes.NpgsqlDbType.Bigint);
				userIdParam.Value = cardAsigneeDto.UserId;

				NpgsqlParameter cardIdParam = new NpgsqlParameter("@card_id", NpgsqlTypes.NpgsqlDbType.Bigint);
				cardIdParam.Value = cardAsigneeDto.CardId;

				cm.Parameters.Add(userIdParam);
				cm.Parameters.Add(cardIdParam);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}
		}
	}
}

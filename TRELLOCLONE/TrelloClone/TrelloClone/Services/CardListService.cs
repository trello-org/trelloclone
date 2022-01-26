using Microsoft.EntityFrameworkCore;
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
	public class CardListService
	{
		private readonly ICardListRepository _cardListRepository;

		public CardListService(ICardListRepository cardListRepository)
		{
			_cardListRepository = cardListRepository;
		}

		internal void UpdateCardList(CardList cardList)
		{
			/*using (var connection = new NpgsqlConnection(_connectionString))
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
			};*/
			_cardListRepository.Update(cardList);
		}

		internal void DeleteCardList(long id)
		{
			_cardListRepository.Remove(id);
		}

		internal void CreateCardListForBoard(CardList cardList)
		{
			/*using (var connection = new NpgsqlConnection(_connectionString))
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
			}*/
			_cardListRepository.Add(cardList);
		}

		
	}
}

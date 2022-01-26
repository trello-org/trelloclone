using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Repository.Repository;
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
		private readonly ICardRepository _cardRepository;

		public CardService(ICardRepository cardRepository)
		{
			_cardRepository = cardRepository;
		}

		internal void CreateCardForCardList(Card card)
		{
			/*using (var connection = new NpgsqlConnection(_connectionString))
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
			}*/
			_cardRepository.Add(card);
		}

		internal void DeleteCard(long id)
		{
			_cardRepository.Remove(id);
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
			_cardRepository.AssignCard(cardAsigneeDto);
		}

		internal void RemoveAssigneeFromCard(CardAsigneeDto cardAsigneeDto)
		{
			_cardRepository.RemoveAssigneeFromCard(cardAsigneeDto);
		}
	}
}

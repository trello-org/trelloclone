using Application.Services.Interfaces;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloClone.Models;


namespace TrelloClone.Services
{
	public class CardService : ICardService
	{
		private readonly ICardRepository _cardRepository;

		public CardService(ICardRepository cardRepository)
		{
			_cardRepository = cardRepository;
		}

		public void Add(Card card)
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

		public void Remove(long id)
		{
			_cardRepository.Remove(id);
		}
		public void AssignCard(long cardId, long userId)
		{
			_cardRepository.AssignCard(cardId, userId);
		}

		public void RemoveAssigneeFromCard(long cardId, long userId)
		{
			_cardRepository.RemoveAssigneeFromCard(cardId, userId);
		}

		public Card GetById(long id)
		{
			return _cardRepository.GetById(id);
		}

		public IEnumerable<Card> GetAll()
		{
			return _cardRepository.GetAll();
		}

		public IEnumerable<Card> Find(Expression<Func<Card, bool>> expression)
		{
			return _cardRepository.Find(expression);
		}

		public void AddRange(IEnumerable<Card> entities)
		{
			throw new NotImplementedException();
		}

		public void RemoveRange(IEnumerable<Card> entities)
		{
			throw new NotImplementedException();
		}

		public void Update(Card entity)
		{
			_cardRepository.Update(entity);
		}
	}
}

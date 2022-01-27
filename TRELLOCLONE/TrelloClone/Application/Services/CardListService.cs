
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
	public class CardListService : ICardListService
	{
		private readonly ICardListRepository _cardListRepository;

		public CardListService(ICardListRepository cardListRepository)
		{
			_cardListRepository = cardListRepository;
		}

		public void Update(CardList cardList)
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

		public void Remove(long id)
		{
			_cardListRepository.Remove(id);
		}

		public void Add(CardList cardList)
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

		public CardList GetById(long id)
		{
			return _cardListRepository.GetById(id);
		}

		public IEnumerable<CardList> GetAll()
		{
			return _cardListRepository.GetAll();
		}

		public IEnumerable<CardList> Find(Expression<Func<CardList, bool>> expression)
		{
			return _cardListRepository.Find(expression);
		}

		public void AddRange(IEnumerable<CardList> entities)
		{
			throw new NotImplementedException();
		}

		public void RemoveRange(IEnumerable<CardList> entities)
		{
			throw new NotImplementedException();
		}

		public async Task<CardList> GetByIdAsync(long id)
		{
			return await _cardListRepository.GetByIdAsync(id);
		}

		public async Task<IEnumerable<CardList>> GetAllAsync()
		{
			return await _cardListRepository.GetAllAsync();
		}

		public async Task<IEnumerable<CardList>> FindAsync(Expression<Func<CardList, bool>> expression)
		{
			return await _cardListRepository.FindAsync(expression);
		}

		public async Task AddAsync(CardList entity)
		{
			await _cardListRepository.AddAsync(entity);
		}

		public async Task AddRangeAsync(IEnumerable<CardList> entities)
		{
			await _cardListRepository.AddRangeAsync(entities);
		}

		public async Task RemoveAsync(long id)
		{
			await _cardListRepository.RemoveAsync(id);
		}

		public Task RemoveRangeAsync(IEnumerable<CardList> entities)
		{
			throw new NotImplementedException();
		}

		public async Task UpdateAsync(CardList entity)
		{
			await _cardListRepository.UpdateAsync(entity);
		}
	}
}

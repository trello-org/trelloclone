using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrelloClone;
using TrelloClone.Models;
using TrelloClone.Models.Dtos;

namespace Repository.Repository
{
	public class CardRepository : ICardRepository
	{
		private readonly ApplicationContext _dbContext;
		private readonly string _connectionString;

		public CardRepository(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
			_connectionString = Environment.GetEnvironmentVariable("adoString");
		}
		public void Add(Card entity)
		{
			_dbContext.Cards.Add(entity);
			_dbContext.SaveChanges();
		}

		public async Task AddAsync(Card entity)
		{
			await _dbContext.Cards.AddAsync(entity);
			await _dbContext.SaveChangesAsync();
		}

		public void AddRange(IEnumerable<Card> entities)
		{
			_dbContext.Cards.AddRange(entities);
			_dbContext.SaveChanges();
		}

		public async Task AddRangeAsync(IEnumerable<Card> entities)
		{
			await _dbContext.Cards.AddRangeAsync(entities);
			await _dbContext.SaveChangesAsync();
		}

		public void AssignCard(long cardId, long userId)
		{
			using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("INSERT INTO public.asignees(user_id, card_id) VALUES(@user_id, @card_id); ", connection);

				NpgsqlParameter userIdParam = new NpgsqlParameter("@user_id", NpgsqlTypes.NpgsqlDbType.Bigint);
				userIdParam.Value = userId;

				NpgsqlParameter cardIdParam = new NpgsqlParameter("@card_id", NpgsqlTypes.NpgsqlDbType.Bigint);
				cardIdParam.Value = cardId;

				cm.Parameters.Add(userIdParam);
				cm.Parameters.Add(cardIdParam);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}
		}

		public Task AssignCardAsync(long cardId, long userId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Card> Find(Expression<Func<Card, bool>> expression)
		{
			return _dbContext.Cards.Where(expression);
		}

		public async Task<IEnumerable<Card>> FindAsync(Expression<Func<Card, bool>> expression)
		{
			return await _dbContext.Cards.Where(expression).ToListAsync();
		}

		public IEnumerable<Card> GetAll()
		{
			return _dbContext.Cards;
		}

		public async Task<IEnumerable<Card>> GetAllAsync()
		{
			return await _dbContext.Cards.ToListAsync();
		}

		public Card GetById(long id)
		{
			return _dbContext.Cards.SingleOrDefault(c => c.Id == id);
		}

		public async Task<Card> GetByIdAsync(long id)
		{
			return await _dbContext.Cards.SingleOrDefaultAsync(c => c.Id == id);
		}

		public void Remove(long id)
		{
			Card toBeRemoved = _dbContext.Cards
				.Where(u => u.Id == id)
				.Include(l => l.Labels)
				.FirstOrDefault();

			_dbContext.Labels.RemoveRange(toBeRemoved.Labels);
			_dbContext.Cards.Remove(toBeRemoved);
			_dbContext.SaveChanges();
		}

		public void RemoveAssigneeFromCard(long cardId, long userId)
		{
			using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("delete from asignees where user_id = @user_id and card_id = @card_id;", connection);

				NpgsqlParameter userIdParam = new NpgsqlParameter("@user_id", NpgsqlTypes.NpgsqlDbType.Bigint);
				userIdParam.Value = userId;

				NpgsqlParameter cardIdParam = new NpgsqlParameter("@card_id", NpgsqlTypes.NpgsqlDbType.Bigint);
				cardIdParam.Value = cardId;

				cm.Parameters.Add(userIdParam);
				cm.Parameters.Add(cardIdParam);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}
		}

		public Task RemoveAssigneeFromCardAsync(long cardId, long userId)
		{
			throw new NotImplementedException();
		}

		public async  Task RemoveAsync(long id)
		{
			Card toBeRemoved = await _dbContext.Cards
				.Where(u => u.Id == id)
				.Include(l => l.Labels)
				.FirstOrDefaultAsync ();

			_dbContext.Labels.RemoveRange(toBeRemoved.Labels);
			_dbContext.Cards.Remove(toBeRemoved);
			await _dbContext.SaveChangesAsync();
		}

		public void RemoveRange(IEnumerable<Card> entities)
		{
			throw new NotImplementedException();
		}

		public Task RemoveRangeAsync(IEnumerable<Card> entities)
		{
			throw new NotImplementedException();
		}

		public void Update(Card entity)
		{
			_dbContext.Cards.Update(entity);
			_dbContext.SaveChanges();
		}

		public async Task UpdateAsync(Card entity)
		{
			_dbContext.Cards.Update(entity);
			await _dbContext.SaveChangesAsync();
		}
	}
}

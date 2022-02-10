using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Repository.EntityTypeConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrelloClone;
using TrelloClone.Models;

namespace Repository.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly ApplicationContext _dbContext;
		private readonly ConnectionStrings _connectionStrings;
		private readonly IMemoryCache _memoryCache;

		public UserRepository(ApplicationContext dbContext, ConnectionStrings connectionStrings, IMemoryCache memoryCache)
		{
			_dbContext = dbContext;
			_connectionStrings = connectionStrings;
			_memoryCache = memoryCache;
		}

		public async Task AddAsync(User entity)
		{
			await _dbContext.AddAsync(entity);
			await _dbContext.SaveChangesAsync();
		}

		public Task<User> Authenticate(string username, string password)
		{
			return _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);

		}

		public async Task<int> CountUsersAsync()
		{
			var cacheKey = "userCount";
			//checks if cache entries exists
			if (!_memoryCache.TryGetValue(cacheKey, out int userCount))
			{
				//calling the server
				userCount = await _dbContext.Users.CountAsync();

				//setting up cache options
				var cacheExpiryOptions = new MemoryCacheEntryOptions
				{
					AbsoluteExpiration = DateTime.Now.AddSeconds(50),
					Priority = CacheItemPriority.High,
					SlidingExpiration = TimeSpan.FromSeconds(20)
				};
				//setting cache entries
				_memoryCache.Set(cacheKey, userCount, cacheExpiryOptions);
			}
			return userCount;
		}

		public Task<List<User>> FindAsync(Expression<Func<User, bool>> expression) => 
			_dbContext.Users.Where(expression).ToListAsync();

		public Task<User> FindByUsernameAsync(string username)
		{
			return _dbContext.Users.SingleOrDefaultAsync(u => u.Username.Equals(username));
		}

		public Task<List<User>> GetAllAsync()
		{
			return _dbContext.Users.ToListAsync();
		}

		public Task<User> GetByIdAsync(long id)
		{
			return _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
		}

		public void Remove(long id)
		{
			User toBeRemoved = _dbContext.Users
				.Where(u => u.Id == id)
				/*.Include(b => b.Boards)
				.ThenInclude(cl => cl.CardLists)
				.ThenInclude(c => c.Cards)
				.ThenInclude(l => l.Labels)*/
				.FirstOrDefault();
			/*foreach (Board b in toBeRemoved.Boards)
			{
				foreach (CardList cl in b.CardLists)
				{
					foreach (Card c in cl.Cards)
					{
						_dbContext.Labels.RemoveRange(c.Labels);
					}
					_dbContext.Cards.RemoveRange(cl.Cards);
				}
				_dbContext.CardLists.RemoveRange(b.CardLists);
			}
			_dbContext.Boards.RemoveRange(toBeRemoved.Boards);*/
			_dbContext.Users.Remove(toBeRemoved);
			_dbContext.SaveChanges();

		}

		public async Task RemoveAsync(long id)
		{
			User toBeRemoved = await _dbContext.Users
				.Where(u => u.Id == id)
				.Include(b => b.Boards)
				.ThenInclude(cl => cl.CardLists)
				.ThenInclude(c => c.Cards)
				.ThenInclude(l => l.Labels)
				.FirstOrDefaultAsync();
			foreach (Board b in toBeRemoved.Boards)
			{
				foreach (CardList cl in b.CardLists)
				{
					foreach (Card c in cl.Cards)
					{
						_dbContext.Labels.RemoveRange(c.Labels);
					}
					_dbContext.Cards.RemoveRange(cl.Cards);
				}
				_dbContext.CardLists.RemoveRange(b.CardLists);
			}
			_dbContext.Boards.RemoveRange(toBeRemoved.Boards);
			_dbContext.Users.Remove(toBeRemoved);
			await _dbContext.SaveChangesAsync();
		}

		public Task RemoveRangeAsync(IEnumerable<User> entities)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(User entity)
		{
			_dbContext.Users.Update(entity);
			return _dbContext.SaveChangesAsync();
			
		}

		public Task<RefreshToken> GetTokenByTokenString(string token)
		{
			return _dbContext.Tokens.SingleOrDefaultAsync(t => t.Token == token);
		}

		public Task UpdateTokenAsync(RefreshToken token)
		{
			_dbContext.Tokens.Update(token);
			return _dbContext.SaveChangesAsync();
		}

		public async Task AddTokenAsync(RefreshToken token)
		{
			await _dbContext.Tokens.AddAsync(token);
			await _dbContext.SaveChangesAsync();
		}
	}
}

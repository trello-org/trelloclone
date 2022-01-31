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
	public class UserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public Task<User> GetByIdAsync(long id)
		{
			return _userRepository.GetByIdAsync(id);
		}

		public Task<List<User>> GetAllAsync()
		{
			return _userRepository.GetAllAsync();
		}

		public Task<List<User>> FindAsync(Expression<Func<User, bool>> expression)
		{
			return _userRepository.FindAsync(expression);
		}

		public Task AddAsync(User entity)
		{
			return _userRepository.AddAsync(entity);
		}

		public Task RemoveAsync(long id)
		{
			return _userRepository.RemoveAsync(id);
		}

		public Task RemoveRangeAsync(IEnumerable<User> entities)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(User entity)
		{
			return _userRepository.UpdateAsync(entity);
		}
	}
}

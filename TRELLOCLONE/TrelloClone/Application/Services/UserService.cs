using Application.Dtos;
using Application.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace Application.Services
{
	public class UserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public Task<int> CountUsersAsync() => _userRepository.CountUsersAsync();

		public Task<User> FindByUsernameAsync(string username)
		{
			return _userRepository.FindByUsernameAsync(username);
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

		public Task<User> Authenticate(string username, string password)
		{
			return _userRepository.Authenticate(username, password);
		}

		public JwtToken GenerateJwtToken(User user)
		{
			// generate token that is valid for 7 days
			/*var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes("mylittlesecretkeyneedstobelongenough");
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] {
					new Claim("id", user.Id.ToString()),
					new Claim(ClaimTypes.Role, user.Role)
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var ret = new JwtToken() { Token = tokenHandler.WriteToken(token), expiresOn = (DateTime)tokenDescriptor.Expires };
			return ret;*/
			// authentication successful so generate jwt token
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes("mylittlesecretkeyneedstobelongenough");
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim("id", user.Id.ToString()),
					new Claim(ClaimTypes.Role, user.Role)
				}),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var tokenString = tokenHandler.WriteToken(token);
			var retToken = new JwtToken() { Token = tokenString };
			return retToken;
		}
	}
}

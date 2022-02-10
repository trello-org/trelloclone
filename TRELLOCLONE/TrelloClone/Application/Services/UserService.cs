using Application.Dtos;
using Application.Services.Interfaces;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
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

		public async Task<JwtToken> GenerateJwtToken(User user, string ipAdress)
		{
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
			var retToken = new JwtToken() { Token = tokenString};
			var refreshToken = GenerateRefreshToken(ipAdress, user.Id);
			await _userRepository.AddTokenAsync(refreshToken);
			retToken.RefreshToken = refreshToken.Token;
			return retToken;
		}

		public async Task<JwtToken> RefreshToken(string token, string ipAddress)
		{
			//var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
			var refreshToken = await _userRepository.GetTokenByTokenString(token);
			var user = await _userRepository.GetByIdAsync(refreshToken.UserId);
			// return null if no user found with token
			if (user == null) return null;

			// return null if token is no longer active
			if (!refreshToken.IsActive) return null;

			// replace old refresh token with a new one and save
			var newRefreshToken = GenerateRefreshToken(ipAddress, user.Id);
			refreshToken.Revoked = DateTime.UtcNow;
			refreshToken.RevokedByIp = ipAddress;
			refreshToken.ReplacedByToken = newRefreshToken.Token;
			//user.RefreshTokens.Add(newRefreshToken);
			//_userRepository.UpdateAsync(user);
			//_userRepository.SaveChanges();
			await _userRepository.UpdateTokenAsync(refreshToken);

			// generate new jwt
			var jwtToken = await GenerateJwtToken(user, ipAddress);
			jwtToken.RefreshToken = newRefreshToken.Token;

			return jwtToken;
		}

		public async Task<bool> RevokeToken(string token, string ipAddress)
		{
			//var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
			var refreshToken = await _userRepository.GetTokenByTokenString(token);
			var user = await _userRepository.GetByIdAsync(refreshToken.UserId);
			// return false if no user found with token
			if (user == null) return false;

			//var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

			// return false if token is not active
			if (!refreshToken.IsActive) return false;

			// revoke token and save
			refreshToken.Revoked = DateTime.UtcNow;
			refreshToken.RevokedByIp = ipAddress;
			await _userRepository.UpdateTokenAsync(refreshToken);
			//_context.UpdateAsync(user);
			//_context.SaveChangesAsync();

			return true;
		}

		private RefreshToken GenerateRefreshToken(string ipAddress, long userId)
		{
			using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				var randomBytes = new byte[64];
				rngCryptoServiceProvider.GetBytes(randomBytes);
				return new RefreshToken
				{
					Token = Convert.ToBase64String(randomBytes),
					Expires = DateTime.UtcNow.AddDays(7),
					Created = DateTime.UtcNow,
					CreatedByIp = ipAddress,
					UserId = userId
				};
			}
		}
	}
}

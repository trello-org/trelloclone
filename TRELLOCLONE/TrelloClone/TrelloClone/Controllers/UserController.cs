using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Filters;
using TrelloClone.Models;
using Application.Services;
using TrelloClone.Exceptions;

using Domain.Constants;
using TrelloClone.Security;
using Microsoft.AspNetCore.Http;
using Application.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrelloClone.Controllers
{
	
	[Route("api/users")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserService _userService;
		private readonly ILogger<UserController> _logger;

		public UserController(UserService userService, ILogger<UserController> logger)
		{
			_userService = userService;
			_logger = logger;
		}

		// GET: api/users
		[HttpGet]
		[AuthorizeAttribute(Role = Role.Admin)]
		[TypeFilter(typeof(LogAttribute))]
		public async Task<IEnumerable<User>> GetAsync()
		{
			_logger.LogInformation("Fetching all users..");
			var users = await _userService.GetAllAsync();
			_logger.LogInformation("Successfully fetched users.");
			return users;
		}

		[HttpGet("count")]
		public async Task<Dictionary<string, int>> CountAsync()
		{
			var retMap = new Dictionary<string, int>();
			retMap.Add("userCount", await _userService.CountUsersAsync());
			return retMap;
		}


		// GET api/users/5
		[HttpGet("{id}")]
		[TypeFilter(typeof(LogAttribute))]
		public async Task<User> GetUserByIdAsync(long id)
		{
			_logger.LogInformation($"Fetching user with ID {id}");
			var user = await _userService.GetByIdAsync(id);
			if (user == null)
			{
				_logger.LogInformation($"Could not find user with ID {id}");
				return user;
			}
			_logger.LogInformation($"User successfully fetchedddddd.");
			return await _userService.GetByIdAsync(id);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] AuthenticateRequest req)
		{
			var user = await _userService.Authenticate(req.Username, req.Password);
			if (user == null) throw new AppException("Invalid credentials");
			var token = await _userService.GenerateJwtToken(user, IpAddress());
			//Console.WriteLine($"{token.Token} {token.expiresOn}");
			SetTokenCookie(token.RefreshToken);
			return Ok(token);

		}

		// POST api/users
		[HttpPost]
		public async Task Post([FromBody] User user)
		{
			_logger.LogInformation("Adding new user..");
			await _userService.AddAsync(user);
			_logger.LogInformation("Successfully added new user.");
		}

		// PUT api/users
		[HttpPut]
		public async Task PutAsync([FromBody] User user)
		{
			_logger.LogInformation("Updating user information..");
			await _userService.UpdateAsync(user);
			_logger.LogInformation("Successfully updated user information.");
		}

		// DELETE api/users/5
		[HttpDelete("{id}")]
		public async Task DeleteAsync(long id)
		{
			_logger.LogInformation($"Deleting user with id {id}");
			await _userService.RemoveAsync(id);
			_logger.LogInformation("Successfully deleted user.");
		}

		
		[HttpPost("refresh-token")]
		public async Task<IActionResult> RefreshToken()
		{
			var refreshToken = Request.Cookies["refreshToken"];
			var response = await _userService.RefreshToken(refreshToken, IpAddress());

			if (response == null)
				return Unauthorized(new { message = "Invalid token" });

			SetTokenCookie(response.RefreshToken);

			return Ok(response);
		}

		[HttpPost("revoke-token")]
		public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest model)
		{
			// accept token from request body or cookie
			var token = model.Token ?? Request.Cookies["refreshToken"];

			if (string.IsNullOrEmpty(token))
				return BadRequest(new { message = "Token is required" });

			var response = await _userService.RevokeToken(token, IpAddress());

			if (!response)
				return NotFound(new { message = "Token not found" });

			return Ok(new { message = "Token revoked" });
		}

		private void SetTokenCookie(string token)
		{
			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Expires = DateTime.UtcNow.AddDays(7)
			};
			Response.Cookies.Append("refreshToken", token, cookieOptions);
		}

		private string IpAddress()
		{
			if (Request.Headers.ContainsKey("X-Forwarded-For"))
				return Request.Headers["X-Forwarded-For"];
			else
				return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
		}
	}
}

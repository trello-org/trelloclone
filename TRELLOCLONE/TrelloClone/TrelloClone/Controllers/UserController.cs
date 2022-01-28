using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;
using TrelloClone.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrelloClone.Controllers
{
	[Route("api/users")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserService _userService;

		public UserController(UserService userService)
		{
			_userService = userService;
		}

		// GET: api/users
		[HttpGet]
		public async Task<IEnumerable<User>> GetAsync()
		{
			return await _userService.GetAllAsync();
		}

		// GET api/users/5
		[HttpGet("{id}")]
		public async Task<User> GetUserByIdAsync(long id)
		{
			return await _userService.GetByIdAsync(id);
		}

		// POST api/users
		[HttpPost]
		public async Task Post([FromBody] User user)
		{
			await _userService.AddAsync(user);
		}

		// PUT api/users
		[HttpPut]
		public async Task PutAsync([FromBody] User user)
		{
			await _userService.UpdateAsync(user);
		}

		// DELETE api/users/5
		[HttpDelete("{id}")]
		public async Task DeleteAsync(long id)
		{
			await _userService.RemoveAsync(id);
		}
	}
}

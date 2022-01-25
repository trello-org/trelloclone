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
		public IEnumerable<User> Get()
		{
			return _userService.GetAllUsers();
		}

		// GET api/users/5
		[HttpGet("{id}")]
		public User GetUserById(long id)
		{
			return _userService.GetUserById(id);
		}

		[HttpGet("boards/{userId}")]
		public IEnumerable<Board> GetAllBoardsForUser(long userId)
		{
			return _userService.GetAllBoardsForUser(userId);
		}

		// POST api/users
		[HttpPost]
		public void Post([FromBody] User user)
		{
			_userService.CreateUser(user);
		}

		// PUT api/users
		[HttpPut]
		public void Put([FromBody] User user)
		{
			_userService.EditUser(user);
		}

		// DELETE api/users/5
		[HttpDelete("{id}")]
		public void Delete(long id)
		{
			_userService.DeleteUser(id);
		}
	}
}

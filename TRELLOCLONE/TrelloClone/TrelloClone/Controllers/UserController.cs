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
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		// GET: api/users
		[HttpGet]
		public IEnumerable<User> Get()
		{
			return _userService.GetAll();
		}

		// GET api/users/5
		[HttpGet("{id}")]
		public User GetUserById(long id)
		{
			return _userService.GetById(id);
		}

		[HttpGet("boards/{userId}")]
		IEnumerable<Board> GetAllBoardsForUser(long userId)
		{
			return null;
		}

		// POST api/users
		[HttpPost]
		public void Post([FromBody] User user)
		{
			_userService.Add(user);
		}

		// PUT api/users
		[HttpPut]
		public void Put([FromBody] User user)
		{
			_userService.Update(user);
		}

		// DELETE api/users/5
		[HttpDelete("{id}")]
		public void Delete(long id)
		{
			_userService.Remove(id);
		}
	}
}

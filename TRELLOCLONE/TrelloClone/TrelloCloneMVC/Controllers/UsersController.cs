using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Services;

namespace TrelloCloneMVC.Controllers
{
	public class UsersController : Controller
	{
		private readonly UserService _userService;

		public UsersController(UserService userService)
		{
			_userService = userService;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _userService.GetAllAsync()); ;
		}

		public IActionResult Create()
		{
			return View();
		}

		public async Task<IActionResult> Edit(int id)
		{
			var user = await _userService.GetByIdAsync(id);
			return View(user);
		}


	}
}

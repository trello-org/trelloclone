using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;
using TrelloClone.Services;

namespace TrelloCloneMVC.Controllers
{
	[Route("accounts")]
	public class AccountsController : Controller
	{
        private readonly UserService _userService;

		public AccountsController(UserService userService)
		{
			_userService = userService;
		}

		[Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            User user = await _userService.FindByUsernameAsync(username);


            if (user.Password.Equals(password))
            {
                HttpContext.Session.SetString("username", username);
                return RedirectToAction("Index", "users");
            }
            else
            {
                ViewBag.error = "Invalid Username and/or password.";
                return View("Index");
            }
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }
    }
}

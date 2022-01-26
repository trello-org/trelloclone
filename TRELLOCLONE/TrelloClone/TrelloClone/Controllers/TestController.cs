using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Services;

namespace TrelloClone.Controllers
{
	[Route("api/test")]
	[ApiController]
	public class TestController : ControllerBase
	{
		private readonly string connectionString = "server=localhost;port=5432;userid=postgres;database=trello;";
		private readonly UserService _userService;
		public TestController(UserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		IActionResult GetAllUsers()
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cm = new SqlCommand("select * from users", connection);

				connection.Open();

				SqlDataReader sdr = cm.ExecuteReader();
				while(sdr.Read())
				{
					Console.WriteLine(sdr["Id"] + " " + sdr["Username"]);
				}
			}

			return Ok();
		}
	}
}

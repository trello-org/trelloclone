using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrelloClone.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdoController : ControllerBase
	{
		private readonly string connectionString = "server=localhost;port=5432;userid=postgres;password=root;database=trello;";

		// GET: api/<AdoController>
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<AdoController>/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			return "value";
		}

		[HttpGet("users")]
		public IEnumerable<User> GetAllUsers()
		{
			var retList = new List<User>();
			using (var connection = new NpgsqlConnection(connectionString))
			{
				var cm = new NpgsqlCommand("select * from users", connection);

				connection.Open();

				NpgsqlDataReader sdr = cm.ExecuteReader();
				while (sdr.Read())
				{
					Console.WriteLine(sdr["Id"] + " " + sdr["Username"]);
					retList.Add(new User
					{
						Username = (string)sdr["Username"],
						Id = (long)sdr["Id"]
					});
				}
			}

			return retList;
		}
		
		// note this is just a test endpoint passing data to Post should be done through body
		[HttpPost("users/{username}")]
		public void CreateNewUser(String username)
		{
			User toCreate = new User();
			toCreate.Username = username;
			using (var connection = new NpgsqlConnection(connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("insert into users (Username) values (@username)", connection);

				NpgsqlParameter usernameParam = new NpgsqlParameter("@username", NpgsqlTypes.NpgsqlDbType.Varchar, username.Length);
				usernameParam.Value = username;

				cm.Parameters.Add(usernameParam);

				cm.Prepare();
				cm.ExecuteNonQuery();

				


			}
		}
		// POST api/<AdoController>
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/<AdoController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/<AdoController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}

		
	}
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace TrelloClone.Services
{
	public class UserService
	{
		private readonly ApplicationContext _dbContext;
		private readonly IConfiguration _configuration;
		private readonly string _connectionString;

		public UserService(IConfiguration configuration)
		{

			_configuration = configuration;
			_connectionString = _configuration["PostgreSql:ConnectionStringADO"];
		}

		internal IEnumerable<User> GetAllUsers()
		{
			var retList = new List<User>();
			using (var connection = new NpgsqlConnection(_connectionString))
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

		internal void CreateUser(User user)
		{
			//if (user.Boards == null) user.Boards = new List<Board>();
			//_dbContext.Add(user);
			//_dbContext.SaveChanges();	
			using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("insert into users (username, password) values (@username, @password)", connection);

				NpgsqlParameter usernameParam = new NpgsqlParameter("@username", NpgsqlTypes.NpgsqlDbType.Varchar, user.Username.Length);
				NpgsqlParameter passwordParam = new NpgsqlParameter("@password", NpgsqlTypes.NpgsqlDbType.Varchar, user.Password.Length);
				usernameParam.Value = user.Username;
				passwordParam.Value = user.Password;

				cm.Parameters.Add(usernameParam);
				cm.Parameters.Add(passwordParam);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}
		}

		internal void EditUser(User user)
		{
			//_dbContext.Update(user);
			//_dbContext.SaveChanges();
			using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("UPDATE public.users SET username =@username, password = @password WHERE id= @id; ", connection);

				NpgsqlParameter usernameParam = new NpgsqlParameter("@username", NpgsqlTypes.NpgsqlDbType.Varchar, user.Username.Length);
				NpgsqlParameter passwordParam = new NpgsqlParameter("@password", NpgsqlTypes.NpgsqlDbType.Varchar, user.Password.Length);
				NpgsqlParameter idParam = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Bigint, (int)user.Id);
				usernameParam.Value = user.Username;
				passwordParam.Value = user.Password;
				idParam.Value = user.Id;

				cm.Parameters.Add(usernameParam);
				cm.Parameters.Add(passwordParam);
				cm.Parameters.Add(idParam);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}

		}

		internal void DeleteUser(long id)
		{
			throw new NotImplementedException();
		}

		internal User GetUserById(long id)
		{
			
			using (var connection = new NpgsqlConnection(_connectionString))
			{
				var cm = new NpgsqlCommand("select * from users where id = @id", connection);
				NpgsqlParameter idParam = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Bigint, (int)id);
				idParam.Value = id;
				connection.Open();

				NpgsqlDataReader sdr = cm.ExecuteReader();
				while (sdr.Read())
				{
					Console.WriteLine(sdr["Id"] + " " + sdr["Username"]);
					return new User
					{
						Username = (string)sdr["Username"],
						Id = (long)sdr["Id"],
						Password = (string)sdr["Password"]
					};
				}
			}
			return null;
		}

		internal IEnumerable<Board> GetAllBoardsForUser(long userId)
		{
			throw new NotImplementedException();
		}
	}
}

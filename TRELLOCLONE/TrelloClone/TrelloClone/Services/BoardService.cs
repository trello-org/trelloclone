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
	public class BoardService
	{
		private readonly ApplicationContext _dbContext;
		private readonly IConfiguration _configuration;
		private readonly string _connectionString;

		public BoardService(IConfiguration configuration)
		{
			_configuration = configuration;
			_connectionString = _configuration["PostgreSql:ConnectionStringADO"];
		}

		internal IEnumerable<Board> GetAllBoardsForUser(long id)
		{
			var retList = new List<Board>();
			using (var connection = new NpgsqlConnection(_connectionString))
			{
				var cm = new NpgsqlCommand("select * from boards where user_id = @id", connection);

				NpgsqlParameter idParam = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Bigint);
				idParam.Value = id;
				cm.Parameters.Add(idParam);

				connection.Open();

				NpgsqlDataReader sdr = cm.ExecuteReader();
				while (sdr.Read())
				{
					Console.WriteLine(sdr["Id"] + " " + sdr["Username"]);
					retList.Add(new Board
					{
						Name = (string)sdr["name"],
						BackgroundUrl = (string)sdr["background_url"],
						Id = (long)sdr["id"],
						Description = (string)sdr["description"]
					}) ;
				}
			}

			return retList;
		}

		internal Board GetBoardById(long id)
		{
			using (var connection = new NpgsqlConnection(_connectionString))
			{
				var cm = new NpgsqlCommand("select * from boards where id = @id", connection);

				NpgsqlParameter idParam = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Bigint, (int)id);
				idParam.Value = id;
				cm.Parameters.Add(idParam);

				connection.Open();

				NpgsqlDataReader sdr = cm.ExecuteReader();
				while (sdr.Read())
				{
					Console.WriteLine(sdr["Id"] + " " + sdr["Username"]);
					return new Board
					{
						Name = (string)sdr["name"],
						BackgroundUrl = (string)sdr["background_url"],
						Id = (long)sdr["id"],
						Description = (string)sdr["description"]
					};
				}
			}
			return null;
		}

		internal void CreateBoard(long userId, Board board)
		{
			using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("insert into board (name, description, background_url, user_id) values (@name, @desc, @url, @user_id)", connection);

				NpgsqlParameter nameParam = new NpgsqlParameter("@name", NpgsqlTypes.NpgsqlDbType.Varchar, board.Name.Length);
				nameParam.Value = board.Name;

				NpgsqlParameter descParam = new NpgsqlParameter("@desc", NpgsqlTypes.NpgsqlDbType.Varchar, board.Description.Length);
				descParam.Value = board.Description;

				NpgsqlParameter userParam = new NpgsqlParameter("@user_id", NpgsqlTypes.NpgsqlDbType.Bigint, (int)userId);
				userParam.Value = board.Description;

				NpgsqlParameter urlParam = new NpgsqlParameter("@url", NpgsqlTypes.NpgsqlDbType.Varchar, board.BackgroundUrl.Length);
				urlParam.Value = board.BackgroundUrl;

				cm.Parameters.Add(nameParam);
				cm.Parameters.Add(descParam);
				cm.Parameters.Add(userParam);
				cm.Parameters.Add(urlParam);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}
		}

		internal void EditBoard(Board board)
		{
			using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("UPDATE public.boards SET name = @name, description = @desc, background_url = @url WHERE user_id = @id; ", connection);

				NpgsqlParameter nameParam = new NpgsqlParameter("@name", NpgsqlTypes.NpgsqlDbType.Varchar, board.Name.Length);
				nameParam.Value = board.Name;

				NpgsqlParameter descParam = new NpgsqlParameter("@desc", NpgsqlTypes.NpgsqlDbType.Varchar, board.Description.Length);
				descParam.Value = board.Description;

				NpgsqlParameter urlParam = new NpgsqlParameter("@url", NpgsqlTypes.NpgsqlDbType.Varchar, board.BackgroundUrl.Length);
				urlParam.Value = board.BackgroundUrl;

				cm.Parameters.Add(nameParam);
				cm.Parameters.Add(descParam);
				cm.Parameters.Add(urlParam);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}
		}

		internal void DeleteBoard(long id)
		{

			using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("delete from boards where id = @id", connection);

				NpgsqlParameter idParam = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Bigint, 0);
				idParam.Value = id;

				cm.Parameters.Add(idParam);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}
		}
	}
}

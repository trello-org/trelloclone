﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace TrelloClone.Services
{
	public class BoardService
	{
		private readonly IBoardRepository _boardRepository;

		public BoardService(IBoardRepository boardRepository)
		{
			_boardRepository = boardRepository;
		}

		internal IEnumerable<Board> GetAllBoardsForUser(long id)
		{
			/*var retList = new List<Board>();
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

			return retList;*/
			return _boardRepository.GetAllBoardsForUser(id);
		}

		internal Board GetBoardById(long id)
		{
			/*using (var connection = new NpgsqlConnection(_connectionString))
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
			return null;*/
			return _boardRepository.GetById(id);
		}

		internal void CreateBoard(long userId, Board board)
		{
			/*using (var connection = new NpgsqlConnection(_connectionString))
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
			}*/
			_boardRepository.Add(board);
		}

		internal void EditBoard(Board board)
		{
			/*using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("UPDATE public.boards SET name = @name, description = @desc, background_url = @url WHERE user_id = @id; ", connection);

				NpgsqlParameter nameParam = new NpgsqlParameter("@name", NpgsqlTypes.NpgsqlDbType.Varchar, board.Name.Length);
				nameParam.Value = board.Name;

				NpgsqlParameter descParam = new NpgsqlParameter("@desc", NpgsqlTypes.NpgsqlDbType.Varchar, board.Description.Length);
				descParam.Value = board.Description;

				NpgsqlParameter urlParam = new NpgsqlParameter("@url", NpgsqlTypes.NpgsqlDbType.Varchar, board.BackgroundUrl.Length);
				urlParam.Value = board.BackgroundUrl;

				NpgsqlParameter idParam = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Bigint);
				idParam.Value = board.UserId;

				cm.Parameters.Add(nameParam);
				cm.Parameters.Add(descParam);
				cm.Parameters.Add(urlParam);
				cm.Parameters.Add(idParam);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}*/
			_boardRepository.Update(board);
		}

		internal void DeleteBoard(long id)
		{
			_boardRepository.Remove(id);
		}

		internal void EditBoardVisibility(Board board)
		{
			/*using (var connection = new NpgsqlConnection(_connectionString))
			{
				connection.Open();
				var cm = new NpgsqlCommand("UPDATE public.boards SET is_public = @is_public WHERE id = @id; ", connection);

				NpgsqlParameter visibilityParam = new NpgsqlParameter("@name", NpgsqlTypes.NpgsqlDbType.Boolean);
				visibilityParam.Value = board.IsPublic;

				NpgsqlParameter idParam = new NpgsqlParameter("@id", NpgsqlTypes.NpgsqlDbType.Bigint);
				idParam.Value = board.Id;

				cm.Parameters.Add(visibilityParam);
				cm.Parameters.Add(idParam);

				cm.Prepare();
				cm.ExecuteNonQuery();
			}*/
			_boardRepository.EditBoardVisibility(board);
		}
	}
}

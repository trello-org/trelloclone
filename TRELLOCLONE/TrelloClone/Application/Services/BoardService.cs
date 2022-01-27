using Application.Services.Interfaces;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace TrelloClone.Services
{
	public class BoardService : IBoardService
	{
		private readonly IBoardRepository _boardRepository;

		public BoardService(IBoardRepository boardRepository)
		{
			_boardRepository = boardRepository;
		}

		public IEnumerable<Board> GetAllBoardsForUser(long id)
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

		public Board GetById(long id)
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

		public void Add(Board board)
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

		public void Update(Board board)
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

		public void Remove(long id)
		{
			_boardRepository.Remove(id);
		}

		public void EditBoardVisibility(Board board)
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

		public IEnumerable<Board> GetAll()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Board> Find(Expression<Func<Board, bool>> expression)
		{
			return _boardRepository.Find(expression);
		}

		public void AddRange(IEnumerable<Board> entities)
		{
			throw new NotImplementedException();
		}

		public void RemoveRange(IEnumerable<Board> entities)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Board>> GetAllBoardsForUserAsync(long id)
		{
			return await _boardRepository.GetAllBoardsForUserAsync(id);
		}

		public async Task EditBoardVisibilityAsync(Board board)
		{
			await _boardRepository.EditBoardVisibilityAsync(board);
		}

		public async Task<Board> GetByIdAsync(long id)
		{
			return await _boardRepository.GetByIdAsync(id);
		}

		public async Task<IEnumerable<Board>> GetAllAsync()
		{
			return await _boardRepository.GetAllAsync();
		}

		public async Task<IEnumerable<Board>> FindAsync(Expression<Func<Board, bool>> expression)
		{
			return await _boardRepository.FindAsync(expression);
		}

		public async Task AddAsync(Board entity)
		{
			await _boardRepository.AddAsync(entity);
		}

		public Task AddRangeAsync(IEnumerable<Board> entities)
		{
			throw new NotImplementedException();
		}

		public async Task RemoveAsync(long id)
		{
			await _boardRepository.RemoveAsync(id);
		}

		public Task RemoveRangeAsync(IEnumerable<Board> entities)
		{
			throw new NotImplementedException();
		}

		public async Task UpdateAsync(Board entity)
		{
			await _boardRepository.UpdateAsync(entity);
		}
	}
}

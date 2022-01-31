using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;
using TrelloClone.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrelloClone.Controllers
{
	[Route("api/boards")]
	[ApiController]
	public class BoardController : ControllerBase
	{

		private readonly BoardService _boardService;
		private readonly ILogger<BoardController> _logger;

		public BoardController(BoardService boardService, ILogger<BoardController> logger)
		{
			_boardService = boardService;
			_logger = logger;
		}

		// GET: api/boards
		[HttpGet("users/{id}")]
		public async Task<IEnumerable<Board>> GetAllBoardsAsync(long id)
		{
			_logger.LogInformation($"Fetching all boards for user with ID {id}");
			var boards = await _boardService.GetAllBoardsForUserAsync(id);
			_logger.LogInformation("Successfully fetched boards for user.");
			return boards;
		}

		// GET api/boards/5
		[HttpGet("{id}")]
		public async Task<Board> GetAsync(long id)
		{
			_logger.LogInformation($"Fetching board with id {id}");
			var board = await _boardService.GetByIdAsync(id);
			_logger.LogInformation("Successfully fetched board.");
			return board;
		}

		

		// POST api/boards
		[HttpPost]
		public async Task PostAsync([FromBody] Board board)
		{
			_logger.LogInformation("Creating new board..");
			await _boardService.AddAsync(board);
			_logger.LogInformation("Successfully created board.");
		}

		// PUT api/boards
		[HttpPut]
		public async Task PutAsync([FromBody] Board board)
		{
			_logger.LogInformation($"Updating information for board with ID {board.Id}");
			await _boardService.UpdateAsync(board);
			_logger.LogInformation("Successfully updated board information.");
		}

		// DELETE api/<BoardController>/5
		[HttpDelete("{id}")]
		public async Task DeleteAsync(long id) 
		{
			_logger.LogInformation($"Deleting user with id {id}");
			await _boardService.RemoveAsync(id);
			_logger.LogInformation("Successfully deleted user.");
		}
	}
}

using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

		public BoardController(BoardService boardService)
		{
			_boardService = boardService;
		}

		// GET: api/boards
		[HttpGet("users/{id}")]
		public async Task<IEnumerable<Board>> GetAllBoardsAsync(long id)
		{
			return await _boardService.GetAllBoardsForUserAsync(id); 
		}

		// GET api/boards/5
		[HttpGet("{id}")]
		public async Task<Board> GetAsync(long id)
		{
			return await _boardService.GetByIdAsync(id);
		}

		

		// POST api/boards
		[HttpPost("{userId}")]
		public async Task PostAsync(long userId, [FromBody] Board board)
		{
			await _boardService.AddAsync(board);
		}

		// PUT api/boards
		[HttpPut]
		public async Task PutAsync([FromBody] Board board)
		{
			await _boardService.UpdateAsync(board);
		}

		// DELETE api/<BoardController>/5
		[HttpDelete("{id}")]
		public async Task DeleteAsync(long id) 
		{
			await _boardService.RemoveAsync(id);
		}
	}
}

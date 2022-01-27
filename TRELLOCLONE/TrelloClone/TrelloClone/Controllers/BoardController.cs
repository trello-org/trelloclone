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

		private readonly IBoardService _boardService;

		public BoardController(IBoardService boardService)
		{
			_boardService = boardService;
		}

		// GET: api/boards
		[HttpGet("users/{id}")]
		public IEnumerable<Board> GetAllBoards(long id)
		{
			return _boardService.GetAllBoardsForUser(id); 
		}

		// GET api/boards/5
		[HttpGet("{id}")]
		public Board Get(long id)
		{
			return _boardService.GetById(id);
		}

		

		// POST api/boards
		[HttpPost("{userId}")]
		public void Post(long userId, [FromBody] Board board)
		{
			_boardService.Add(board);
		}

		// PUT api/boards
		[HttpPut]
		public void Put([FromBody] Board board)
		{
			_boardService.Update(board);
		}

		// DELETE api/<BoardController>/5
		[HttpDelete("{id}")]
		public void Delete(long id) 
		{
			_boardService.Remove(id);
		}
	}
}

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
		[HttpGet]
		public IEnumerable<Board> GetAllBoards()
		{
			return _boardService.GetAllBoardsForUser(); 
		}

		// GET api/boards/5
		[HttpGet("{id}")]
		public Board Get(Guid id)
		{
			return _boardService.GetBoardById(id);
		}

		

		// POST api/boards
		[HttpPost("{userId}")]
		public void Post(Guid userId, [FromBody] Board board)
		{
			_boardService.CreateBoard(userId, board);
		}

		// PUT api/boards
		[HttpPut]
		public void Put([FromBody] Board board)
		{
			_boardService.EditBoard(board);
		}

		// DELETE api/<BoardController>/5
		[HttpDelete("{id}")]
		public void Delete(Guid id) 
		{
			_boardService.DeleteBoard(id);
		}
	}
}

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
	[Route("api/cardlists")]
	[ApiController]
	public class CardListController : ControllerBase
	{
		private readonly CardListService _cardListService;

		public CardListController(CardListService cardListService)
		{
			_cardListService = cardListService;
		}

		[HttpPut]
		public void Put([FromBody] CardList cardList)
		{
			_cardListService.UpdateCardList(cardList);
		}
		[HttpDelete("id")]
		public void Delete(Guid id)
		{
			_cardListService.DeleteCardList(id);
		}

		[HttpPost("boardId")]
		public void CreateCardListForBoard(Guid boardId, [FromBody] CardList cardList)
		{
			_cardListService.CreateCardListFoBoard(boardId, cardList);
		}

		
	}
}

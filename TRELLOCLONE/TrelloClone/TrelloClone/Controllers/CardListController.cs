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
	[Route("api/cardlists")]
	[ApiController]
	public class CardListController : ControllerBase
	{
		private readonly ICardListService _cardListService;

		public CardListController(ICardListService cardListService)
		{
			_cardListService = cardListService;
		}

		[HttpPut]
		public void Put([FromBody] CardList cardList)
		{
			_cardListService.Update(cardList);
		}
		[HttpDelete("id")]
		public void Delete(long id)
		{
			_cardListService.Remove(id);
		}

		[HttpPost]
		public void CreateCardListForBoard([FromBody] CardList cardList)
		{
			_cardListService.Add(cardList);
		}

		
	}
}

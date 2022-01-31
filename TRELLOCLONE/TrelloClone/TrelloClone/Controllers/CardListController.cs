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
		private readonly CardListService _cardListService;

		public CardListController(CardListService cardListService)
		{
			_cardListService = cardListService;
		}

		[HttpPut]
		public async Task PutAsync([FromBody] CardList cardList)
		{
			await _cardListService.UpdateAsync(cardList);
		}
		[HttpDelete("id")]
		public async Task DeleteAsync(long id)
		{
			await _cardListService.RemoveAsync(id);
		}

		[HttpPost]
		public async Task CreateCardListForBoardAsync([FromBody] CardList cardList)
		{
			await _cardListService.AddAsync(cardList);
		}

		
	}
}

using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;
using Application.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrelloClone.Controllers
{
	[Route("api/cardlists")]
	[ApiController]
	public class CardListController : ControllerBase
	{
		private readonly CardListService _cardListService;
		private readonly ILogger<CardListController> _logger;

		public CardListController(CardListService cardListService, ILogger<CardListController> logger)
		{
			_cardListService = cardListService;
			_logger = logger;
		}

		[HttpPut]
		public async Task PutAsync([FromBody] CardList cardList)
		{
			_logger.LogInformation($"Updating information for card list with ID {cardList.Id}");
			await _cardListService.UpdateAsync(cardList);
			_logger.LogInformation("Successfully updated card list information.");
		}
		[HttpDelete("id")]
		public async Task DeleteAsync(long id)
		{
			_logger.LogInformation($"Deleting Card List with id {id}.");
			await _cardListService.RemoveAsync(id);
			_logger.LogInformation("Successfully deleted card list.");
		}

		[HttpPost]
		public async Task CreateCardListForBoardAsync([FromBody] CardList cardList)
		{
			_logger.LogInformation($"Creating new card list for board {cardList.BoardId}.");
			await _cardListService.AddAsync(cardList);
			_logger.LogInformation("Successfully created card list");
		}

		
	}
}

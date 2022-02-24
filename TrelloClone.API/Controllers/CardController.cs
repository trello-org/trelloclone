using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;
using TrelloClone.Models.Dtos;
using Application.Services;

namespace TrelloClone.Controllers
{
	[Route("api/cards")]
	[ApiController]
	public class CardController : ControllerBase
	{
		private readonly CardService _cardService;
		private readonly ILogger<CardController> _logger;

		public CardController(CardService cardService, ILogger<CardController> logger)
		{
			_cardService = cardService;
			_logger = logger;
		}

		[HttpPost]
		public async Task CreateCardForCardListAsync([FromBody] Card card)
		{
			_logger.LogInformation($"Creating new card for card list {card.Id}");
			await _cardService.AddAsync(card);
			_logger.LogInformation("Successfully created new card");
		}

		[HttpDelete("{id}")]
		public async Task DeleteCardAsync(long id)
		{
			_logger.LogInformation($"Deleting card with id {id}..");
			await _cardService.RemoveAsync(id);
			_logger.LogInformation("Successfully deleted card");
		}


	}
}

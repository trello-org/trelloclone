using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;
using TrelloClone.Models.Dtos;
using TrelloClone.Services;

namespace TrelloClone.Controllers
{
	[Route("api/cards")]
	[ApiController]
	public class CardController : ControllerBase
	{
		private readonly CardService _cardService;

		public CardController(CardService cardService)
		{
			_cardService = cardService;
		}

		[HttpPost]
		public async Task CreateCardForCardListAsync([FromBody] Card card)
		{
			await _cardService.AddAsync(card);
		}

		[HttpDelete("{id}")]
		public async Task DeleteCardAsync(long id)
		{
			await _cardService.RemoveAsync(id);
		}


	}
}

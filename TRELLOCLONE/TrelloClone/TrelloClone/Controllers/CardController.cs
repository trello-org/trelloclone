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

		[HttpPost("{cardListId}")]
		public void CreateCardForCardList(long cardListId, [FromBody] Card card)
		{
			_cardService.CreateCardForCardList(cardListId, card);
		}

		[HttpDelete("{id}")]
		public void DeleteCard(long id)
		{
			_cardService.DeleteCard(id);
		}

		[HttpDelete("nocascade/{id}")]
		public void DeleteCardNoCascade(Guid id)
		{
			_cardService.DeleteCardNoCascade(id);
		}

		[HttpPost("label")]
		public void AddLabelToCard(CardLabelDto cardLabelDto)
		{
			_cardService.AddLabelToCard(cardLabelDto);
		}
	}
}

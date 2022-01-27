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
		private readonly ICardService _cardService;

		public CardController(ICardService cardService)
		{
			_cardService = cardService;
		}

		[HttpPost]
		public void CreateCardForCardList([FromBody] Card card)
		{
			_cardService.Add(card);
		}

		[HttpDelete("{id}")]
		public void DeleteCard(long id)
		{
			_cardService.Remove(id);
		}

		[HttpDelete("nocascade/{id}")]
		void DeleteCardNoCascade(Guid id)
		{
			throw new NotImplementedException();
		}

	}
}

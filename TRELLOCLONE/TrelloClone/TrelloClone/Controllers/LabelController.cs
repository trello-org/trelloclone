using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;
using TrelloClone.Services;

namespace TrelloClone.Controllers
{
	[Route("api/labels")]
	[ApiController]
	public class LabelController : ControllerBase
	{
		private readonly ICardLabelService _labelService;

		public LabelController(ICardLabelService labelService)
		{
			_labelService = labelService;
		}

		[HttpPost]
		public async Task CreateLabelAsync([FromBody] Label label)
		{
			await _labelService.AddAsync(label);
		}
		
		[HttpDelete("{id}")]
		public async Task DeleteLabelAsync(long id)
		{
			await _labelService.RemoveAsync(id);
		}
	}
}

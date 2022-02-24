using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloClone.Models;
using Application.Services;

namespace TrelloClone.Controllers
{
	[Route("api/labels")]
	[ApiController]
	public class LabelController : ControllerBase
	{
		private readonly ICardLabelService _labelService;
		private readonly ILogger<LabelController> _logger;

		public LabelController(ICardLabelService labelService, ILogger<LabelController> logger)
		{
			_labelService = labelService;
			_logger = logger;
		}

		[HttpPost]
		public async Task CreateLabelAsync([FromBody] Label label)
		{
			_logger.LogInformation($"Creating new label for card with id {label.CardId}..");
			await _labelService.AddAsync(label);
			_logger.LogInformation("Successfully created label.");
		}
		
		[HttpDelete("{id}")]
		public async Task DeleteLabelAsync(long id)
		{
			_logger.LogInformation($"Deleting label with id {id}..");
			await _labelService.RemoveAsync(id);
			_logger.LogInformation("Successfully deleted card.");
		}
	}
}

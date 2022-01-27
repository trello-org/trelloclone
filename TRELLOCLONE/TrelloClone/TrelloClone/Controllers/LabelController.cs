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
		public void CreateLabel([FromBody] Label label)
		{
			_labelService.Add(label);
		}
		
		[HttpDelete("{id}")]
		public void DeleteLabel(long id)
		{
			_labelService.Remove(id);
		}
	}
}

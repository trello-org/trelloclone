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
		private readonly LabelService _labelService;

		public LabelController(LabelService labelService)
		{
			_labelService = labelService;
		}

		[HttpPost]
		public void CreateLabel([FromBody] Label label)
		{
			_labelService.CreateLabel(label);
		}
		
		[HttpDelete("{id}")]
		public void DeleteLabel(Guid id)
		{
			_labelService.DeleteLabel(id);
		}
	}
}

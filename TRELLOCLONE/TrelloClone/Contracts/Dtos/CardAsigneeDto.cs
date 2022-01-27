using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Models.Dtos
{
	public class CardAsigneeDto
	{
		public long UserId { get; set; }
		public long CardId { get; set; }
	}
}

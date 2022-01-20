using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Models.Dtos
{
	public class CardLabelDto
	{
		public Guid LabelId { get; set; }
		public Guid CardId { get; set; }
	}
}

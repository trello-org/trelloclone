using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Models.Dtos
{
	public class CardLabelDto
	{
		public long LabelId { get; set; }
		public long CardId { get; set; }
	}
}

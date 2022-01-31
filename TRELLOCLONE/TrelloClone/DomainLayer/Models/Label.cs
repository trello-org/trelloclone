using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Models
{
	[Table("labels")]
	public class Label
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		private long _id;
		[Required]
		private string _name;
		private string _colorHex;
		private long _cardId;

		[Column("id")]
		public long Id { get { return _id; } set { _id = value; } }
		[Column("name")]
		[Required]
		[StringLength(16, ErrorMessage = "Maximum length is 16, Minimum length is 4.", MinimumLength = 4)]
		[RegularExpression("[A-Za-z]{4,16}", ErrorMessage = "Only letters allowed")]
		public string Name { get { return _name; } set { _name = value; } }
		[Column("color_hex")]
		public string ColorHex { get { return _colorHex; } set { _colorHex = value; } }
		[Column("card_id")]
		public long CardId { get { return _cardId; } set { _cardId = value; } }
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Models
{
	public class Label
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		private Guid _id;
		[Required]
		private string _name;
		private string _colorHex;

		public Guid Id { get { return _id; } set { _id = value; } }
		public string Name { get { return _name; } set { _name = value; } }
		public string ColorHex { get { return _colorHex; } set { _colorHex = value; } }

	}
}

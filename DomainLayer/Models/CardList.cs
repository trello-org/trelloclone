using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Models
{
    //[Table("cardlists")]
    public class CardList
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       //[Key]
        private long _id;
        [Required]
        private string _name;
        private IEnumerable<Card> _cards;
        private long _boardId;
        [Column("id")]
        public long Id { get { return _id; } set { _id = value; } }
        [Column("name")]
        [Required]
        [StringLength(16, ErrorMessage = "Maximum length is 16, Minimum length is 4.", MinimumLength = 4)]
        [RegularExpression("[A-Za-z]{4,16}", ErrorMessage = "Only letters allowed")]
        public string Name { get { return _name; } set { _name = value; } }
        [Column("board_id")]
        public long BoardId { get { return _boardId; } set { _boardId = value; } }
        public IEnumerable<Card> Cards { get { return _cards; } set { _cards = value; } }

    }
}

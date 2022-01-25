using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Models
{
    [Table("cards")]
    public class Card
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        private long _id;
        [Required]
        private string _name;
        private string _description;
        //private IEnumerable<Label> _labels;
        private long _cardListId;

        public long Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        //public IEnumerable<Label> Labels { get { return _labels; } set { _labels = value; } }
        public long CardListId { get { return _cardListId; } set { _cardListId = value; } }
        }
    }

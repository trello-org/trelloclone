﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Models
{
    [Table("cardlists")]
    public class CardList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        private long _id;
        [Required]
        private string _name;
        //private IEnumerable<Card> _cards;
        private long _boardId;

        public long Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public long BoardId { get { return _boardId; } set { _boardId = value; } }
        // public IEnumerable<Card> Cards { get { return _cards; } set { _cards = value; } }

    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Models
{
    public class Board
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        private Guid _id;
        [Required]
        private string _name;
        private string _description;
        private string _backgroundUrl;
        private IEnumerable<CardList> _cardLists;

        public Guid Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Description { get { return _description; } set { _description = value; } }
        public string BackgroundUrl { get { return _backgroundUrl; } set { _backgroundUrl = value; } }
        public IEnumerable<CardList> CardLists { get { return _cardLists; } set { _cardLists = value; } }
    }
}
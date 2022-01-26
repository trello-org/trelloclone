using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Models
{
    [Table("boards")]
    public class Board
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        private long _id;
        [Required]
        private string _name;
        private string _description;
        private string _backgroundUrl;
        private bool _isPublic;
        private IEnumerable<CardList> _cardLists;
        private long _userId;

        [Column("id")]
        public long Id { get { return _id; } set { _id = value; } }
        [Column("name")]
        public string Name { get { return _name; } set { _name = value; } }
        [Column("description")]
        public string Description { get { return _description; } set { _description = value; } }
        [Column("background_url")]
        public string BackgroundUrl { get { return _backgroundUrl; } set { _backgroundUrl = value; } }
        [Column("user_id")]
        public long UserId {  get { return _userId; } set { _userId = value; } }
        [Column("is_public")]
        public bool IsPublic { get { return _isPublic; } set { _isPublic = value; } }
        public IEnumerable<CardList> CardLists { get { return _cardLists; } set { _cardLists = value; } }
    }
}

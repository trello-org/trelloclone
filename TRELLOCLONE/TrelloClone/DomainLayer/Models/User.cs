using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Models
{
    [Table("users")]
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
		[Key]
        private long _id;
        [Required]
        [Column("username")]
        private string _username;
        [Required]
        private string _password;
        private IEnumerable<Board> _boards;

        [Column("id")]
        public long Id { get { return _id; } set { _id = value; } }
        [Column("username")]
        public string Username { get { return _username; } set { _username = value; } }
        [Column("password")]
        public string Password { get { return _password; }  set { _password = value; } }

        public IEnumerable<Board> Boards { get { return _boards; } set { _boards = value; } }

    }
}

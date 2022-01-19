using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        [Key]
        private Guid _id;
        [Required]
        private string _username;
        [Required]
        private string _password;
        private IEnumerable<Board> _boards;

        public Guid Id { get { return _id; } set { _id = value; } }
        public string Username { get { return _username; } set { _username = value; } }
        public string Password { set { _password = value; } }

        public IEnumerable<Board> Boards { get { return _boards; } set { _boards = value; } }

    }
}

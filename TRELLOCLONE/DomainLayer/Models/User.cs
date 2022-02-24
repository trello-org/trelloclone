using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrelloClone.Models
{
	//[Table("users")]
    //[Index(nameof(Username), IsUnique = true)]
    public class User
    {
        private long _id;
        private string _username;
        private string _password;
        private IEnumerable<Board> _boards;
        private string _role;

        public long Id { get { return _id; } set { _id = value; } }
        [Required]
        [StringLength(16, ErrorMessage = "Maximum length is 16, Minimum length is 6.", MinimumLength = 6)]
        [RegularExpression("[A-Za-z0-9]+", ErrorMessage = "Only letters and numbers allowed")]
        public string Username { get { return _username; } set { _username = value; } }
        [StringLength(100, ErrorMessage = "Minimum length is 8, max is 100", MinimumLength = 8)]
        [Required]
        public string Password { get { return _password; }  set { _password = value; } }
        public IEnumerable<Board> Boards { get { return _boards; } set { _boards = value; } }
        public string Role { get { return _role; } set { _role = value; } }

    }
}

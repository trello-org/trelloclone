using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloClone.Models;

namespace Repository.Repository
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User> FindByUsernameAsync(string username);
		Task<int> CountUsersAsync();

		public Task<User> Authenticate(string username, string password);
		
	}
}

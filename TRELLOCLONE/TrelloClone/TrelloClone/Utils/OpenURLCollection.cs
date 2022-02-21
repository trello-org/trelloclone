using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Utils
{
	public class OpenURLCollection
	{
		public static List<string> Urls = new List<string>()
		{
			"login",
			"revoke-token",
			"api/users"
		};

		public static bool IsUrlProtected(string url)
		{
			foreach (var u in Urls)
			{
				if (url.Contains(u)) return false;
			}
			return true;
		} 
	}
}

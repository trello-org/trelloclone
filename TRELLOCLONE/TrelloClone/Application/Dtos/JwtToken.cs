using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
	public class JwtToken
	{
		public string Token { get; set; }
		public string RefreshToken { get; set; }
	}
}

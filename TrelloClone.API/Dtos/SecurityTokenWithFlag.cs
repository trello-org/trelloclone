using Application.Dtos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Dtos
{
	public class SecurityTokenWithFlag
	{
		public SecurityToken SecToken;
		public JwtToken TokenPair;
		public bool hasTokenBeenRefreshed;
 	}
}

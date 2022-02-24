using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Exceptions.ExceptionFactory
{
	public interface ICustomException 
	{
		void LogException();
		int GetStatusCode();
		Task CreateResponse(HttpContext context);
	}
}

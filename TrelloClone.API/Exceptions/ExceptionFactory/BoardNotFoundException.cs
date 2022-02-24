using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace TrelloClone.Exceptions.ExceptionFactory
{
	public class BoardNotFoundException : Exception, ICustomException
	{
		public async Task CreateResponse(HttpContext context)
		{
			var response = context.Response;
			response.ContentType = "application/json";
			response.StatusCode = GetStatusCode();
			var result = JsonSerializer.Serialize(new { message = this.Message });
			await response.WriteAsync(result);
		}

		public void LogException()
		{
			throw new NotImplementedException();
		}

		public int GetStatusCode()
		{
			return (int)HttpStatusCode.BadRequest;	
		}

		public void Throw()
		{
			throw this;
		}


	}
}

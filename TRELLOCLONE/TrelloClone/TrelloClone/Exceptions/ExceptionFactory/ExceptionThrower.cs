using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace TrelloClone.Exceptions.ExceptionFactory
{
	public static class ExceptionThrower
	{
        private static int SetStatusCode(Exception ex)
        {
            switch (ex)
            {
                case AppException e:
                    // custom application error
                    if (e.StatusCode == 0) return (int)HttpStatusCode.BadRequest;
                    return e.StatusCode;
                case KeyNotFoundException e:
                    // not found error
                    return (int)HttpStatusCode.NotFound;
                default:
                    // unhandled error
                    return (int)HttpStatusCode.InternalServerError;
            }
        }

        public static async Task CreateJSONResponse(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = SetStatusCode(ex);
            var result = JsonSerializer.Serialize(new { message = ex.Message });
            await response.WriteAsync(result);
        }



    }
}

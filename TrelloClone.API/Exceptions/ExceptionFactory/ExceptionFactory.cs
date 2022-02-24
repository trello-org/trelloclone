using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace TrelloClone.Exceptions.ExceptionFactory
{
	public static class ExceptionFactory
	{
        public static int SetStatusCode(Exception ex)
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
                case DbUpdateConcurrencyException e:
                    return (int)HttpStatusCode.BadRequest;
                default:
                    // unhandled error
                    return (int)HttpStatusCode.InternalServerError;
            }
        }




    }
}

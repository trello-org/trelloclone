using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace TrelloClone.Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode;
        
        public AppException() : base() { }

        public AppException(Exception ex) : base()
		{
            switch (ex)
            {
                case AppException e:
                    // custom application error
                    if (e.StatusCode == 0) e.StatusCode = (int)HttpStatusCode.BadRequest;
                    StatusCode = e.StatusCode;
                    break;
                case KeyNotFoundException e:
                    // not found error
                    StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
        }

        public AppException(string message) : base(message) {
            StatusCode = 400;
        }

        public AppException(string message, int statusCode) : base(message)
		{
            StatusCode = statusCode;
		}

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }

       

    }
}

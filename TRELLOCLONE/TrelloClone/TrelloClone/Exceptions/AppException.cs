using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace TrelloClone.Exceptions
{
    public class AppException : Exception
    {
        public int StatusCode;
        public AppException() : base() { }

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

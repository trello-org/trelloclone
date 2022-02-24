using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Constants;
using System.Web.Http.Results;
using TrelloClone.Models;

namespace TrelloClone.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
	{
        public string Role { get; set; }

        public AuthorizeAttribute()
		{

		}
        public AuthorizeAttribute(string Role) : base()
		{
			this.Role = Role;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];
			Console.WriteLine("using custom authorization..");
            if (user == null || user.Role != Role)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}

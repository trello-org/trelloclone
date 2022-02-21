using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TrelloClone.Filters
{
	public class LogAttribute : Microsoft.AspNetCore.Mvc.Filters.IActionFilter, IAsyncActionFilter
	{
        private readonly ILogger<LogAttribute> _logger;

        public LogAttribute(ILogger<LogAttribute> logger)
		{
            _logger = logger;
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			_logger.LogInformation($"Exited");
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			_logger.LogInformation("Entered");
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			_logger.LogInformation($"Entered");
			await next();
			_logger.LogInformation("Exited");
		}

		/*public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			_logger.LogInformation($"Entered");
			await next();
			_logger.LogInformation("Exited");

		}*/
		/*public override void OnActionExecuting(HttpActionContext actionContext)
{
   _logger.LogInformation($"Entered ${actionContext.ActionDescriptor.ActionName}");
   //Trace.WriteLine(string.Format("Action Method {0} executing at {1}", actionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()), "Web API Logs");
}

public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
{
   _logger.LogInformation($"Successfully executed ${actionExecutedContext.ActionContext.ActionDescriptor.ActionName}");
   //Trace.WriteLine(string.Format("Action Method {0} executed at {1}", actionExecutedContext.ActionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()), "Web API Logs");
}*/
	}
}

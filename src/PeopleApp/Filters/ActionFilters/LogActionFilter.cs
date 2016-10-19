﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
    
namespace PeopleApp.Filters.ActionFilters
{
    /// <summary>
    /// Filter that does logging of events
    /// <remarks>This filter should be extended</remarks>
    /// </summary>
    public class LogActionFilter : ActionFilterAttribute
    {
        private readonly ILogger _logger;

        public LogActionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("LogActionFilter");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogWarning("ClassFilter OnActionExecuting");
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogWarning("ClassFilter OnActionExecuted");
            base.OnActionExecuted(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogWarning("ClassFilter OnResultExecuting");
            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogWarning("ClassFilter OnResultExecuted");
            base.OnResultExecuted(context);
        }
    }
}

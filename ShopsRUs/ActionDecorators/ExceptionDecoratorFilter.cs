using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using ShopsRUs.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUsApi.ActionDecorators
{
    public class ExceptionDecoratorFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionDecoratorFilter> logger;

        public ExceptionDecoratorFilter(ILogger<ExceptionDecoratorFilter> logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context == null)
                return;

            logger.LogError(context.Exception, $"error on {context.HttpContext.Request.Method} {context.HttpContext.Request.Host.Value}{context.HttpContext.Request.Path}");

            if (context.Exception is not CustomException exception)
                return;

            context.HttpContext.Response.StatusCode = (int)exception.HttpStatusCode;
            context.Result = new JsonResult(new { ErrorMessage = exception.Message });
        }
    }
}

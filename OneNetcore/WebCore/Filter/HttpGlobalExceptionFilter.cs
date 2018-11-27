using Common;
using Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebCore
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        readonly IHostingEnvironment _env;
        public HttpGlobalExceptionFilter( IHostingEnvironment env)
        {
            _env = env;
        }
        public void OnException(ExceptionContext context)
        {
            // logger.LogError(new EventId(context.Exception.HResult),
            //context.Exception,
            //context.Exception.Message);
            LogHelp.Error("OnException" + context.Exception.Message);
            var json = new ErrorResponse(context.Exception.Message, (int)HttpStatusCode.InternalServerError);
            if (_env.IsDevelopment()) json.DeveloperMessage = context.Exception;
             context.Result = new JsonResult(new { message = json.Message, state = json.state });
           // context.Result = new ApplicationErrorResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.ExceptionHandled = true;
        }
    }
    public class ApplicationErrorResult : ObjectResult
    {
        public ApplicationErrorResult(object value) : base(value)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }

    public class ErrorResponse
    {
        public ErrorResponse(string msg,int _state)//state
        {
            state = _state;
            Message = msg;
        }
        public int state { get; set; }
        public string Message { get; set; }
        public object DeveloperMessage { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using Rosi.BMS.API.Core.Logging.Serilog;
using Rosi.BMS.API.Core.Utilities.Messages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security;
using System.Threading.Tasks;

namespace Rosi.BMS.API.Core.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IFileLogService _fileLogService;

        public ExceptionMiddleware(RequestDelegate next, IFileLogService fileLogService)
        {
            _next = next;
            _fileLogService = fileLogService;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {     
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }


        private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            _ = e.Message;
            string message = e.Message;
            if (e.GetType() == typeof(ValidationException))
            {
                message = e.Message;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (e.GetType() == typeof(ApplicationException))
            {              
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;      
            }
            else if (e.GetType() == typeof(UnauthorizedAccessException))
            {               
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;           
            }
            else if (e.GetType() == typeof(SecurityException))
            {                
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;                
            }
            else if (e.GetType() == typeof(NotSupportedException))
            {              
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;          
            }
            else
            {
                message = ExceptionMessage.InternalServerError;
            }
            _fileLogService.Error($"{httpContext.Response.StatusCode.ToString()}-{message}");
            await httpContext.Response.WriteAsync(message);
        }
    }
}
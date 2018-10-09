using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Ledger.WebApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILoggerFactory loggerFactory)
        {
            ILogger logger = loggerFactory.CreateLogger("Exception");

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                httpContext.Response.Clear();
                httpContext.Response.ContentType = "text/plain";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await httpContext.Response.WriteAsync("There was an error processing your request. This error was logged and saved to the server.");
            }
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}

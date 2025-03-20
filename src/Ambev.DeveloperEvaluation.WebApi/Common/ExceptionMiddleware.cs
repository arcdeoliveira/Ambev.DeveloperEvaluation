using Ambev.DeveloperEvaluation.Common.Json;
using Ambev.DeveloperEvaluation.WebApi.Middleware;

namespace Ambev.DeveloperEvaluation.WebApi.Common
{
    public abstract class ExceptionMiddleware<TData> where TData : class   
    {
        protected readonly RequestDelegate _next;
        protected readonly ILogger<ExceptionUnhandleMiddleware> _logger;
        protected readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionUnhandleMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public abstract Task InvokeAsync(HttpContext context);

        internal async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = context.Response.StatusCode;

            var data = GetData(context, exception);
            var jsonResponse = data.Serialize();

            await context.Response.WriteAsync(jsonResponse);
        }

        internal abstract TData GetData(HttpContext context, Exception exception);
    }
}

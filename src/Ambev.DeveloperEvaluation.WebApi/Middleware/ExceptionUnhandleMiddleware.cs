using System.Net;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Common.Response;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware
{
    public class ExceptionUnhandleMiddleware : ExceptionMiddleware<ApiErrorResponse>
    {
        public ExceptionUnhandleMiddleware(RequestDelegate next, ILogger<ExceptionUnhandleMiddleware> logger, IHostEnvironment env) 
        : base(next, logger, env) { }

        public override async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        internal override ApiErrorResponse GetData(HttpContext context, Exception exception)
        {
            if (context is null || exception is null)
                return default!;

            var statusCode = context.Response.StatusCode;
            var isDevelopment = _env.IsDevelopment();   

            var message = (HttpStatusCode)statusCode switch
            {
                HttpStatusCode.Unauthorized => "Unauthorized access.",
                HttpStatusCode.Forbidden => "Forbidden access.",
                HttpStatusCode.NotFound => "Resource not found.",
                _ => "An unexpected error occurred.",
            };

            _logger.LogWarning(exception, "Error {statusCode}: {message}", message, statusCode);

            var apiErrorResponse = new ApiErrorResponse(statusCode)
            {
                ErrorMessage = isDevelopment ? exception.Message     : message,
                StackTrace   = isDevelopment ? exception.StackTrace! : string.Empty
            };

            return apiErrorResponse;
        }
    }
}

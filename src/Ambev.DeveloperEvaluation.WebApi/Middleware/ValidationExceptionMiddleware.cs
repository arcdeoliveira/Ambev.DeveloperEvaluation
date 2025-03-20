using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Common.Response;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware
{
    public class ValidationExceptionMiddleware : ExceptionMiddleware<ApiResponse>
    {
        private ValidationException validationException;

        public ValidationExceptionMiddleware(RequestDelegate next, ILogger<ExceptionUnhandleMiddleware> logger, IHostEnvironment env)
        : base(next, logger, env) 
        {
            validationException = default!;
        }

        public override async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                validationException = ex;
                await HandleExceptionAsync(context, ex);
            }
        }

        internal override ApiResponse GetData(HttpContext context, Exception exception)
        {
            var message = "Validation Failed";
            _logger.LogWarning(exception, "Error {statusCode}: {message}", message, context.Response.StatusCode);

            return new ApiResponse
            {
                Success = false,
                Message = message,
                Errors = validationException.Errors
                    .Select(error => (ValidationErrorDetail)error)
            };
        }
    }
}

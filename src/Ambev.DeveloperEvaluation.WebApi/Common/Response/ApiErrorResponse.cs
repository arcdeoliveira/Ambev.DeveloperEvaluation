namespace Ambev.DeveloperEvaluation.WebApi.Common.Response
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse() { }

        public ApiErrorResponse(int statusCode) 
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; } = 0;
        public string ErrorMessage { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
    }
}

namespace Ambev.DeveloperEvaluation.WebApi.Common.Response;

public class ApiResponseWithData<T> : ApiResponse
{
    public T? Data { get; set; }
}

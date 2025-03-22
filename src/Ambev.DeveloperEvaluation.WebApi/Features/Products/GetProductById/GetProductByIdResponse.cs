namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById
{
    public class GetProductByIdResponse
    {
        public string Id { get;  set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string Name { get;  set; } = string.Empty;
        public string Description { get;  set; } = string.Empty;
        public decimal Price { get;  set; } = 0;
        public string Status { get;  set; } = string.Empty;
    }
}

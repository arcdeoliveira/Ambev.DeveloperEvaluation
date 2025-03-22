namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    public class UpdateProductResponse
    {
        public string Id { get; set; } = string.Empty;
        public string CreatedAt { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; } = 0;
        public string Status { get; private set; } = string.Empty;
    }
}

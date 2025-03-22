using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductResult
    {
        public string Id { get; set; } = string.Empty;
        public DateTime CreatedAt { get; private set; } = DateTime.MinValue;
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; } = 0;
        public ProductStatus Status { get; private set; } = ProductStatus.Unknown;
    }
}

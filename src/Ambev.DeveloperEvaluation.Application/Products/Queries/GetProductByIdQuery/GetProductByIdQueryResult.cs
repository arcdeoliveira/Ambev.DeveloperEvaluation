using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductByIdQuery
{
    public class GetProductByIdQueryResult
    {
        public string Id { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public ProductStatus Status { get; set; } = ProductStatus.Unknown;
    }
}

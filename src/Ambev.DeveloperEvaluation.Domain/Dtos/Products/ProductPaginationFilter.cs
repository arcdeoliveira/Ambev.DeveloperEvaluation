using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Dtos.Products
{
    public class ProductPaginationFilter
    {
        public DateTime? CreatedInitial { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? Price { get; private set; }
        public ProductStatus? Status { get; private set; }
    }
}

using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleResponse
    {
        public string Id { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal Total { get; set; } = 0;
        public int AfiliateId { get; set; } = 0;

        public string NotFoundProductMessage { get; set; } = string.Empty;
        public string IneligibleProductMessage { get; set; } = string.Empty;

        public IEnumerable<ProductSaleGetByIdDto> NewProducts { get; set; } = [];
    }
}

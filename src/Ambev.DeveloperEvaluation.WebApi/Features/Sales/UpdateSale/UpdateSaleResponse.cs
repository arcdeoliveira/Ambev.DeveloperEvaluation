using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleResponse
    {
        public string Id { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal Total { get; set; } = 0;
        public int AfiliateId { get; set; } = 0;
        public string WarningNotFoundProductMessage { get; set; } = string.Empty;
        public string WarningIneligibleProductMessage { get; set; } = string.Empty;

        public IEnumerable<ProductSaleUpdateDto> ProductSales { get; set; } = [];
    }
}

using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem
{
    public class CancelSaleItemResponse
    {
        public string Id { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
        public decimal Total { get; set; } = 0;
        public string Status { get; set; } = string.Empty;
        public int AfiliateId { get; set; } = 0;
        public Guid UserId { get; set; } = Guid.Empty;
        public string NotFoundMessage { get; set; } = string.Empty;

        public IEnumerable<ProductSaleGetByIdDto> ProductSales { get; set; } = [];
    }
}

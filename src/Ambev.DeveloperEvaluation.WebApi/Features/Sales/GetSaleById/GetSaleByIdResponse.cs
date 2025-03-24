using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    public class GetSaleByIdResponse
    {
        public string Id { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } =string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal Total { get; set; } = 0;
        public Guid UserId { get; set; } = Guid.Empty;
        public int AfiliateId { get; set; } = 0;

        public IEnumerable<ProductSaleGetByIdDto> ProductSales { get; set; } = [];
    }
}

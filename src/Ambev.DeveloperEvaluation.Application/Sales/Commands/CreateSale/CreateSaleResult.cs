using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    public class CreateSaleResult
    {
        public string Id { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
        public SaleStatus Status { get; set; } = SaleStatus.Ordered;
        public decimal Total { get; set; } = 0;
        public Guid UserId { get; set; } = Guid.Empty;
        public int AfiliateId { get; set; } = 0;

        public string NotFoundProductMessage { get; set; } = string.Empty;
        public string IneligibleProductMessage { get; set; } = string.Empty;

        public IEnumerable<ProductSale> ProductSales { get; set; } = [];
    }
}

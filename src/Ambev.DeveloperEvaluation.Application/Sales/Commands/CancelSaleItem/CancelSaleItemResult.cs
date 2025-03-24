using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSaleItem
{
    public class CancelSaleItemResult
    {
        public string Id { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
        public DateTime? UpdatedAt { get; set; }
        public decimal Total { get; set; } = 0;
        public SaleStatus Status { get; set; } = SaleStatus.None;
        public int AfiliateId { get; set; } = 0;
        public Guid UserId { get; set; } = Guid.Empty;
        public string NotFoundMessage { get; set; } = string.Empty;

        public IEnumerable<ProductSale> ProductSales { get; set; } = [];
    }
}

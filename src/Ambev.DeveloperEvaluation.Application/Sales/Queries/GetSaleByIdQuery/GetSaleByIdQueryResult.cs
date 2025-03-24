using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleByIdQuery
{
    public class GetSaleByIdQueryResult
    {
        public string Id { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public SaleStatus Status { get; set; } = SaleStatus.Ordered;
        public decimal Total { get; set; } = 0;
        public Guid UserId { get; set; } = Guid.Empty;
        public int AfiliateId { get;  set; } = 0;

        public IEnumerable<ProductSale> ProductSales { get;  set; } = [];
    }
}

using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale
{
    public class UpdateSaleResult
    {
        public string Id { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
        public SaleStatus Status { get; set; } = SaleStatus.None;
        public decimal Total { get; set; } = 0;
        public int AfiliateId { get; set; } = 0;
        public string WarningNotFoundProductMessage { get; set; } = string.Empty;
        public string WarningIneligibleProductMessage { get; set; } = string.Empty; 

        public IEnumerable<ProductSale> ProductSales { get; set; } = [];

    }
}

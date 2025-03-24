using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Dtos.Sales
{
    public class SalePaginationDto
    {
        public string Id { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;    
        public SaleStatus Status { get; set; } = SaleStatus.None;
        public decimal Total { get; set; } = decimal.Zero;
        public int AfiliateId { get; set; } = 0;  
        public string AfilliateName { get; set; } = string.Empty;   
        public Guid UserId { get; set; } = Guid.Empty;
        public string UserName { get; set; } = string.Empty;    
        public DateTime? UpdatedAt { get; set; }   

        public IEnumerable<ProductSalePaginationDto> ProductSales { get; set; } = [];
    }
}

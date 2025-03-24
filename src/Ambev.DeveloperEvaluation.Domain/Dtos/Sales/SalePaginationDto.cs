using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;

namespace Ambev.DeveloperEvaluation.Domain.Dtos.Sales
{
    public class SalePaginationDto
    {
        public string Id { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;    
        public string Status { get; set; } = string.Empty;
        public decimal Total { get; set; } = decimal.Zero;
        public int AfiliateId { get; set; } = 0;  
        public string AfilliateName { get; set; } = string.Empty;   
        public Guid UserId { get; set; } = Guid.Empty;
        public string UserName { get; set; } = string.Empty;    
        public string UpdatedAt { get; set; } = string.Empty;   

        public IEnumerable<ProductSalePaginationDto> ProductSales { get; set; } = [];
    }
}

namespace Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales
{
    public class ProductSalePaginationDto
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; } = 0;
        public decimal UnitDiscount { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public decimal Total { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
        public bool Canceled { get; set; }
        public DateTime? DateCanceled { get; set; } 
    }
}

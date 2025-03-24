namespace Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales
{
    public class ProductSaleUpdateDto
    {
        public string ProductId { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; } = 0;
        public decimal UnitDiscount { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public decimal Total { get; set; } = 0;
        
    }
}

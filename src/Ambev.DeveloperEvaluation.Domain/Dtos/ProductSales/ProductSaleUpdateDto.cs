namespace Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales
{
    public class ProductSaleUpdateDto
    {
        public string ProductId { get; private set; } = string.Empty;
        public decimal UnitPrice { get; private set; } = 0;
        public decimal UnitDiscount { get; private set; } = 0;
        public int Quantity { get; set; } = 0;
        public decimal Total { get; private set; } = 0;
        
    }
}

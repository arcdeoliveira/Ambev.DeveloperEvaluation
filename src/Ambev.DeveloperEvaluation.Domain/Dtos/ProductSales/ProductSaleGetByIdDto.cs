namespace Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales
{
    public class ProductSaleGetByIdDto
    {
        public string ProductId { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; } = 0;
        public decimal UnitDiscount { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public decimal Total { get;  set; } = 0;
        public string CreatedAt { get; set; } = string.Empty;
        public string Canceled { get;  set; } = string.Empty;       
        public string DateCanceled { get;  set; } = string.Empty;
    }
}

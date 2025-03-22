namespace Ambev.DeveloperEvaluation.Domain.Dtos.Products
{
    public class ProductPaginationDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }  = decimal.Zero; 
    }
}

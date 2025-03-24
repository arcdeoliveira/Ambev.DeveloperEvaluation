using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequest
    {
        public string Id { get; set; } = string.Empty;
        public int? AfiliateId { get; set; }     
        public IEnumerable<ProductSaleDto> NewProductIds { get; set; } = [];
    }
}

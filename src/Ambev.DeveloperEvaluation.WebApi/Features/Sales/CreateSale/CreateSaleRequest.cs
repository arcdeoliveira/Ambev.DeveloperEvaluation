using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public int? AfiliateId { get; set; }
        public IEnumerable<ProductSaleDto> Products { get; set; } = [];

    }
}

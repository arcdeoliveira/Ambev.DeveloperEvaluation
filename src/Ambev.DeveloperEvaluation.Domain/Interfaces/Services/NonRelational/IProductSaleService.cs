using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational
{
    public interface IProductSaleService
    {
        string CancelProductsInSale(IEnumerable<string> productIdsToCancel, IEnumerable<ProductSale> productSales);
        int GetMaxQuantityAllowedToSell();
        string GenerateIneligibleProductIdsMessage(IEnumerable<string> productIds, int maxQuantityAllowedToSell);
        Task<IEnumerable<ProductSale>> ProcessProductSales(IEnumerable<ProductSaleDto> productSaleDtos);
    }
}

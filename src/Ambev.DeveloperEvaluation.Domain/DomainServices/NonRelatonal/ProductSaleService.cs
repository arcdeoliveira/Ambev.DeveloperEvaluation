using System.Text;
using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational;

namespace Ambev.DeveloperEvaluation.Domain.DomainServices.NonRelatonal
{
    public class ProductSaleService : IProductSaleService
    {
        private readonly IProductService _productService;

        public const int MaxQuantityAllowedToSell = 20;

        public ProductSaleService(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IEnumerable<ProductSale>> ProcessProductSales(IEnumerable<ProductSaleDto> productSaleDtos)
        {
            if (productSaleDtos == null)
                return [];

            var productSaleList = new List<ProductSale>();   

            foreach (var productSaleDto in productSaleDtos)
            {

                var product = await _productService.GetByIdAsync(productSaleDto.ProductId);
                if (product == null)
                    continue;

                var productSale = new ProductSale(product.Price, productSaleDto.Quantity, productSaleDto.ProductId);
                productSale.SetDiscountByQuantity();
                productSale.SetTotal();
            }

            return productSaleList;  
        }

        public string GenerateIneligibleProductIdsMessage(IEnumerable<string> productIds,int maxQuantityAllowedToSell)
        {
            var message = new StringBuilder($"Product(s) not allowed for sale. Maximum {MaxQuantityAllowedToSell} quantities per product: ");
            foreach (var productId in productIds)
            {
                message.AppendLine(productId.ToString());
            }

            return message.ToString();
        }

        public int GetMaxQuantityAllowedToSell() => 20;

        public string CancelProductsInSale(IEnumerable<string> productIdsToCancel, IEnumerable<ProductSale> productSalesList)
        {
            if (productIdsToCancel.Any() || productSalesList.Any()) 
                return "None product to cancel in the sale.";

            var countProductIdNotFound = 0; 
            var cancelMessageBuilder = new StringBuilder("Product Id not found in the sale.");

            foreach (var productId in productIdsToCancel)
            {
                var productSale = productSalesList.FirstOrDefault(x => x.ProductId == productId);
                if (productSale == null)
                {
                    countProductIdNotFound++;   
                    cancelMessageBuilder.AppendLine(productId);

                    continue;   
                }

                productSale.Cancel();   
            }

            return countProductIdNotFound > 0 ? cancelMessageBuilder.ToString() : string.Empty;
        }
    }
}

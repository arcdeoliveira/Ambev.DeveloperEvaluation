using System.Text;

namespace Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales
{
    public class ProductSaleDto
    {
        public string ProductId { get; set; } = string.Empty;   
        public int Quantity { get; set; }



        public static bool AllProductIdsValids(IEnumerable<ProductSaleDto> products)
        {
            return products.All(p => !string.IsNullOrEmpty(p.ProductId));
        }

        public static bool AllProductQuantityValid(IEnumerable<ProductSaleDto> products)
        {
            return products.All(product => product.Quantity > 0);
        }


        public static string GetInvalidProductQuantitiesMessage(IEnumerable<ProductSaleDto> products)
        {
            var stringBuider = new StringBuilder(string.Empty);

            return products.Aggregate(stringBuider, (current, product) =>
            {
                if (product.Quantity == 0)
                    current.AppendLine($"Product ID {product.ProductId} quantity must be informed.");
                return current;
            }).ToString();
        }


        public static string GetInvalidProductIdsMessage(IEnumerable<ProductSaleDto> products)
        {
            var stringBuider = new StringBuilder(string.Empty);

            return products.Aggregate(stringBuider, (current, product) =>
            {
                if (string.IsNullOrEmpty(product.ProductId))
                    current.AppendLine($"Product ID {product.ProductId} is invalid.");
                return current;
            }).ToString();
        }
    }
}

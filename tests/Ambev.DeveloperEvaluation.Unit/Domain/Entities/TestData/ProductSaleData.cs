using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class ProductSaleData
    {
        private static readonly Faker<ProductSale> _productSaleFaker = new Faker<ProductSale>().CustomInstantiator(faker => BuildSale(faker));

        private static ProductSale BuildSale(Faker faker)
        {
            var productSale = new ProductSale();

            productSale.AlterProduct(faker.Random.Guid().ToString());
            productSale.AlterUnitPrice(faker.Random.Decimal(1, 300));
            productSale.AlterQuantity(faker.Random.Int(1, 20));

            productSale.SetDiscountByQuantity();
            productSale.SetTotal();

            return productSale;
        }

        public static IEnumerable<ProductSale> GenerateValidProductSales(int quantity)
        {
            return _productSaleFaker.Generate(quantity, null);
        }
    }
}

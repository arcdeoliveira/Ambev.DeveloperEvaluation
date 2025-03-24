using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class SaleTestData
    {
        private static readonly Faker<Sale> saleFaker = new Faker<Sale>().CustomInstantiator(faker => BuildSale(faker));

        private static Sale BuildSale(Faker faker)
        {
            var sale = new Sale();

            sale.AlterId(faker.Random.Guid().ToString());
            sale.AlterCostumer(faker.Random.Guid());
            sale.AlterAffiliate(faker.Random.Short(0, 1000));

            var productSale = ProductSaleData.GenerateValidProductSales(5);

            sale.AlterItens(productSale);
            sale.SetTotal();

            return sale;
        }


        public static Sale GenerateValidSale()
        {
            return saleFaker.Generate();
        }
    }
}

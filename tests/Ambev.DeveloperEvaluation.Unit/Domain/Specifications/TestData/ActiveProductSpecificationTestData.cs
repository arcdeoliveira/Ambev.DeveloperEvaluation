using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications.TestData
{
    public static class ActiveProductSpecificationTestData
    {
        private static readonly Faker<Product> _productFaker = new Faker<Product>().CustomInstantiator(faker => BuildProduct(faker));

        public static Product CreateProductWithStatus(ProductStatus productStatus)
        {
            var product = _productFaker.Generate();
            product.AlterStatus(productStatus);

            return product;
        }


        private static Product BuildProduct(Faker faker)
        {
            var product = new  Product();

            product.AlterName(faker.Commerce.ProductName());
            product.AlterDescription(faker.Commerce.ProductDescription());
            product.AlterPrice(faker.Random.Decimal(1, 500));
            product.AlterStatus(faker.PickRandom<ProductStatus>());

            return product;
        }
    }
}

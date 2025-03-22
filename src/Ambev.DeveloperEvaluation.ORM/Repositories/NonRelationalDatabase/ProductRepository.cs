using Ambev.DeveloperEvaluation.Domain.Dtos.Products;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Context;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.NonRelationalDatabase;
using Ambev.DeveloperEvaluation.ORM.Repositories.Common;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.ORM.Repositories.NonRelationalDatabase
{
    public class ProductRepository : BaseMongoDBRepository<Product>, IProductRepository
    {
        const string COLLECTION_NAME = "products";

        public ProductRepository(IMongoDbContext context) : base(context, COLLECTION_NAME)
        {
        }

        public async Task<IEnumerable<ProductPaginationDto>> GetProductsWithPagination(
            FilterDefinition<Product> filter, 
            ProjectionDefinition<Product, ProductPaginationDto> projection, 
            int skip, 
            int limit)
        {
            IEnumerable<ProductPaginationDto> result;

            await Task.FromResult(result = _collection
               .Find(filter)
               .Project(projection)
               .Skip(skip)
               .Limit(limit)
               .ToEnumerable());

            return result;  
        }
    }
}

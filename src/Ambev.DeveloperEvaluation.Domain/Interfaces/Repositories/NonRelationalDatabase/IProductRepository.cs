using Ambev.DeveloperEvaluation.Domain.Dtos.Products;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.Common;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.NonRelationalDatabase
{
    public interface IProductRepository : IBaseMongoDBRepository<Product>  
    {
        Task<IEnumerable<ProductPaginationDto>> GetProductsWithPagination(
            FilterDefinition<Product> filter, ProjectionDefinition<Product, ProductPaginationDto> projection, int skip, int limit);
    }
}

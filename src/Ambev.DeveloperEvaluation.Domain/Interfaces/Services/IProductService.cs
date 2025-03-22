using Ambev.DeveloperEvaluation.Domain.Dtos.Products;
using Ambev.DeveloperEvaluation.Domain.Dtos.Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Common;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Services
{
    public interface IProductService : IBaseNonRelationalService<Product>
    {
        Task<bool> CheckIfNameCanBeUpdate(string id, string name, CancellationToken cancellationToken);
        Task<BaseDataPaginationDto<ProductPaginationDto>> GetAllProductsWithPagination(
            FilterDefinition<Product> filter,
            ProjectionDefinition<Product, ProductPaginationDto> projection,            
            int pageNumber, int pageSize,
            CancellationToken cancellationToken);

        Task<bool> ProductNameAlreadyExist(string name, CancellationToken cancellationToken);
    }
}

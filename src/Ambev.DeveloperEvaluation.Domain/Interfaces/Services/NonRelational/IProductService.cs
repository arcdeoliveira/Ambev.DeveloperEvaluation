using Ambev.DeveloperEvaluation.Domain.Dtos.Products;
using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;
using Ambev.DeveloperEvaluation.Domain.Dtos.Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Common;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational
{
    public interface IProductService : IBaseNonRelationalService<Product>
    {
        Task<bool> CheckIfNameCanBeUpdate(string id, string name, CancellationToken cancellationToken);
        Task<BaseDataPaginationDto<ProductPaginationDto>> GetAllProductsWithPagination(
            FilterDefinition<Product> filter,
            ProjectionDefinition<Product, ProductPaginationDto> projection,            
            int pageNumber, int pageSize,
            CancellationToken cancellationToken);

        Task<ProductSaleEligibilityStatusDto> GetEligibleProductsForSale(
            IEnumerable<ProductSaleDto> productSaleDtos,
            int quantityAllowedToSell, 
            CancellationToken cancellationToken);

        string GenerateNotFoundProductIdsMessage(IEnumerable<string> productIds);
        Task<bool> ProductNameAlreadyExist(string name, CancellationToken cancellationToken);
    }
}

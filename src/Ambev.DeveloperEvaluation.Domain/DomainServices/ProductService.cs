using Ambev.DeveloperEvaluation.Domain.DomainServices.Common;
using Ambev.DeveloperEvaluation.Domain.Dtos.Products;
using Ambev.DeveloperEvaluation.Domain.Dtos.Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.NonRelationalDatabase;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.Domain.DomainServices
{
    public class ProductService : BaseNonRelationalService<Product>, IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<bool> ProductNameAlreadyExist(string name, CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.Eq(u => u.Name, name);

            return await DocumentExist(filter, cancellationToken);
        }

        public async Task<bool> CheckIfNameCanBeUpdate(string id, string name, CancellationToken cancellationToken)
        {
            var builder = Builders<Product>.Filter;
            var filters = new List<FilterDefinition<Product>>
            {
                builder.Ne(x => x.Id, id),
                builder.Eq(x => x.Name, name)
            };

            var filter = builder.And(filters);

            return await DocumentExist(filter, cancellationToken);
        }

        public async Task<BaseDataPaginationDto<ProductPaginationDto>> GetAllProductsWithPagination(
            FilterDefinition<Product> filter, 
            ProjectionDefinition<Product, ProductPaginationDto> projection,             
            int pageNumber, int pageSize,
            CancellationToken cancellationToken)
        {
            if (filter is null || projection is null)
                return default!;

            var total = await _repository.CountDocuments(filter, cancellationToken);

            var skip = (pageNumber - 1) * pageSize;
            var limit = pageSize;            
            var data = await _repository.GetProductsWithPagination(filter, projection, skip, limit);

            return new BaseDataPaginationDto<ProductPaginationDto>(data, total);
        }
    }
}

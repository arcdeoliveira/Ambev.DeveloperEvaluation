using Ambev.DeveloperEvaluation.Domain.Dtos.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.Common;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.NonRelationalDatabase
{
    public interface ISaleRepository : IBaseMongoDBRepository<Sale>
    {
        Task<IEnumerable<SalePaginationDto>> RetrieveSalesWithPagination(
            FilterDefinition<Sale> filter,
            ProjectionDefinition<Sale, SalePaginationDto> projection,
            int skip,
            int limit);
    }
}

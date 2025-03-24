using Ambev.DeveloperEvaluation.Domain.Dtos.Sales;
using Ambev.DeveloperEvaluation.Domain.Dtos.Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Common;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational
{
    public interface ISaleService : IBaseNonRelationalService<Sale> 
    {
        Task<BaseDataPaginationDto<SalePaginationDto>> GetPaginatedSaleList(
           FilterDefinition<Sale> filter,
           ProjectionDefinition<Sale, SalePaginationDto> projection,
           int pageNumber, int pageSize,
           CancellationToken cancellationToken);

        Task UpdateSaleProdcutsAsync(Sale sale, IEnumerable<ProductSale> itens, Guid userId, short? afiliateId);
    }
}

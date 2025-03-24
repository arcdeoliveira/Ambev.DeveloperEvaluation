using Ambev.DeveloperEvaluation.Domain.DomainServices.Common;
using Ambev.DeveloperEvaluation.Domain.Dtos.Sales;
using Ambev.DeveloperEvaluation.Domain.Dtos.Shared;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.NonRelationalDatabase;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.Domain.DomainServices.NonRelatonal
{
    public class SaleService : BaseNonRelationalService<Sale>, ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        protected SaleService(ISaleRepository saleRepository) : base(saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<BaseDataPaginationDto<SalePaginationDto>> GetPaginatedSaleList(FilterDefinition<Sale> filter, 
            ProjectionDefinition<Sale, SalePaginationDto> projection, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            if (filter is null || projection is null)
                return default!;

            var total = await _saleRepository.CountDocuments(filter, cancellationToken);

            var skip = (pageNumber - 1) * pageSize;
            var limit = pageSize;
            var data = await _saleRepository.RetrieveSalesWithPagination(filter, projection, skip, limit);

            return new BaseDataPaginationDto<SalePaginationDto>(data, total);
        }

        public async Task UpdateSaleProdcutsAsync(Sale sale, IEnumerable<ProductSale> itens, Guid userId, short? affiliateId)
        {
            if(affiliateId.HasValue)
                sale.AlterAffiliate(affiliateId.Value);

            if(userId != Guid.Empty && userId != sale.UserId)
                sale.AlterCostumer(userId);

            if (itens.Any())
            {
                sale.AlterItens(itens);
                sale.SetTotal();
            }
       
            sale.AlterStatus(SaleStatus.Modified);
            sale.AlterDateUpdate();

            await UpdateAsync(sale.Id, sale);
        }
    }
}
 
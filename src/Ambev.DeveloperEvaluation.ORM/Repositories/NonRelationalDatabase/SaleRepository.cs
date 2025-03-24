using Ambev.DeveloperEvaluation.Domain.Dtos.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.NonRelationalDatabase;
using Ambev.DeveloperEvaluation.ORM.Contexts;
using Ambev.DeveloperEvaluation.ORM.Repositories.Common;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.ORM.Repositories.NonRelationalDatabase
{
    public class SaleRepository : BaseMongoDBRepository<Sale>, ISaleRepository
    {
        const string COLLECTION_NAME = "sales";

        protected SaleRepository(MongoDBContext context) : base(context, COLLECTION_NAME)
        {
        }

        public async Task<IEnumerable<SalePaginationDto>> RetrieveSalesWithPagination(FilterDefinition<Sale> filter, 
            ProjectionDefinition<Sale, SalePaginationDto> projection, int skip, int limit)
        {
            IEnumerable<SalePaginationDto> result;

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

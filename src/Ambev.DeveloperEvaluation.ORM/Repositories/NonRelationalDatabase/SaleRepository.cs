using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.NonRelationalDatabase;
using Ambev.DeveloperEvaluation.ORM.Contexts;
using Ambev.DeveloperEvaluation.ORM.Repositories.Common;

namespace Ambev.DeveloperEvaluation.ORM.Repositories.NonRelationalDatabase
{
    public class SaleRepository : BaseMongoDBRepository<Sale>, ISaleRepository
    {
        const string COLLECTION_NAME = "sales";

        protected SaleRepository(MongoDBContext context) : base(context, COLLECTION_NAME)
        {
        }
    }
}

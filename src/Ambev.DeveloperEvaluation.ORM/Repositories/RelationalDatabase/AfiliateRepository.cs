using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.RelationalDatabase;
using Ambev.DeveloperEvaluation.ORM.Contexts;
using Ambev.DeveloperEvaluation.ORM.Repositories.Common;

namespace Ambev.DeveloperEvaluation.ORM.Repositories.RelationalDatabase
{
    public class AfiliateRepository : BaseRelationalDatabaseRepository<Afiliate>, IAfiliateRepository
    {
        protected AfiliateRepository(PostgreContext context) : base(context)
        {
        }
    }
}

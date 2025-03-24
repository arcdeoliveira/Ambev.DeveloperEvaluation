using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.RelationalDatabase;
using Ambev.DeveloperEvaluation.ORM.Contexts;
using Ambev.DeveloperEvaluation.ORM.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories.RelationalDatabase
{
    public class AfiliateRepository : BaseRelationalDatabaseRepository<Affiliate>, IAffiliateRepository
    {
        protected AfiliateRepository(PostgreContext context) : base(context)
        {
        }

        public async Task<string?> GetAfiliateNameAsync(short afiliateId, CancellationToken cancellationToken)
        {
            return await (from affiliate in _context.Afiliates.AsNoTracking()
                    where affiliate.Id == afiliateId
                    select affiliate.Name).FirstOrDefaultAsync(cancellationToken);
        }
    }
}

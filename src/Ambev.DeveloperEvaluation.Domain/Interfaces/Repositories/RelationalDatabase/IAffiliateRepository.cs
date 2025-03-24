using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.Common;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.RelationalDatabase
{
    public interface IAffiliateRepository : IBaseRelationalDatabaseRepository<Affiliate>
    {
        Task<string?> GetAfiliateNameAsync(short afiliateId, CancellationToken cancellationToken);
    }
}

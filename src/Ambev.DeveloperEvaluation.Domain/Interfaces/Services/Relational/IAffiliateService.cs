using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Common;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Relational
{
    public interface IAffiliateService : IBaseRelationalService<Affiliate> 
    {
        Task<string?> GetAfiliateNameAsync(short afiliateId, CancellationToken cancellationToken);
    }
}

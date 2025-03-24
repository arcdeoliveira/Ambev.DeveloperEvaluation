using Ambev.DeveloperEvaluation.Domain.DomainServices.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.RelationalDatabase;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Relational;

namespace Ambev.DeveloperEvaluation.Domain.DomainServices.Relational
{
    public class AffiliateService : BaseRelationalService<Affiliate>, IAffiliateService
    {
        private readonly IAffiliateRepository _affiliateRepository;
        public AffiliateService(IAffiliateRepository repository) : base(repository)
        {
            _affiliateRepository = repository;
        }

        public async Task<string?> GetAfiliateNameAsync(short afiliateId, CancellationToken cancellationToken)
        {
            return await _affiliateRepository.GetAfiliateNameAsync(afiliateId, cancellationToken);
        }
    }
}

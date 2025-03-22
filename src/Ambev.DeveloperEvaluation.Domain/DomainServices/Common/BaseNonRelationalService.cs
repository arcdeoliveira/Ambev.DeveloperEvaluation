using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.Common;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Common;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.Domain.DomainServices.Common
{
    public class BaseNonRelationalService<TDocument> : IBaseNonRelationalService<TDocument> where TDocument : BaseDocument
    {
        private readonly IBaseMongoDBRepository<TDocument> _baseRepository;

        protected BaseNonRelationalService(IBaseMongoDBRepository<TDocument>  baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task CreateAsync(TDocument document)
        {
            await _baseRepository.CreateAsync(document);
        }

        public async Task DeleteAsync(string id)
        {
           await _baseRepository.DeleteAsync(id);
        }

        public async Task<bool> DocumentExist(FilterDefinition<TDocument> filter, CancellationToken cancellationToken)
        {
            var quantity = await _baseRepository.CountDocuments(filter, cancellationToken);

            return quantity > 0;
        }

        public async Task<bool> DocumentExist(string id, CancellationToken cancellationToken)
        {
            var exist = await _baseRepository.DocumentExist(id, cancellationToken);

            return exist > 0;
        }

        public async Task<IEnumerable<TDocument>> FindAsync(Expression<Func<TDocument, bool>> filter)
        {
            return await _baseRepository.FindAsync(filter); 
        }

        public async Task<TDocument> GetByIdAsync(string id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }      

        public async Task UpdateAsync(string id, TDocument entity)
        {
            await _baseRepository.UpdateAsync(id, entity);
        }
    }
}

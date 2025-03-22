using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Common;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.Common
{
    public interface IBaseMongoDBRepository<TDocument> where TDocument : BaseDocument
    {
        Task<long> CountDocuments(FilterDefinition<TDocument> filter, CancellationToken cancellationToken);
        Task<long> DocumentExist(string id, CancellationToken cancellationToken);
        Task<TDocument> GetByIdAsync(string id);
        Task<IEnumerable<TDocument>> FindAsync(Expression<Func<TDocument, bool>> filter);
        Task CreateAsync(TDocument entity);
        Task UpdateAsync(string id, TDocument entity);
        Task DeleteAsync(string id);
    }
}

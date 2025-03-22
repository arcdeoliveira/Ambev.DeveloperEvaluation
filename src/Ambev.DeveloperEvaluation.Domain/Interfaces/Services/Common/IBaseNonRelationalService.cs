using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Common;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Common
{
    public interface IBaseNonRelationalService<TDocument> where TDocument : BaseDocument
    {
        Task<TDocument> GetByIdAsync(string id);
        Task<bool> DocumentExist(string id, CancellationToken cancellationToken);
        Task<bool> DocumentExist(FilterDefinition<TDocument> filter, CancellationToken cancellationToken);
        Task<IEnumerable<TDocument>> FindAsync(Expression<Func<TDocument, bool>> filter);
        Task CreateAsync(TDocument document);
        Task UpdateAsync(string id, TDocument document);
        Task DeleteAsync(string id);
    }
}

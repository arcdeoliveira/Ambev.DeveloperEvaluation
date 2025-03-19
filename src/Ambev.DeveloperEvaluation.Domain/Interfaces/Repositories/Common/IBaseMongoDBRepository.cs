using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.Common
{
    public interface IBaseMongoDBRepository<TEntity> where TEntity : BaseMongoDBEntity
    {
        Task<TEntity> GetByIdAsync(string id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter);
        Task<(IEnumerable<TEntity> Items, long TotalCount)> GetPagedAsync(Expression<Func<TEntity, bool>> filter, int pageNumber, int pageSize);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(string id, TEntity entity);
        Task DeleteAsync(string id);
    }
}

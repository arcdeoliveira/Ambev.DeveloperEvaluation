using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.Common
{
    public interface IBaseRelationalDatabaseRepository<TEntity> where TEntity : class 
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task SaveAsync(CancellationToken cancellationToken);

        Task<TEntity?> GetByIdAsync(object id);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        IEnumerable<TEntity> GetAllAsync(Func<TEntity, bool> predicate);
    }
}

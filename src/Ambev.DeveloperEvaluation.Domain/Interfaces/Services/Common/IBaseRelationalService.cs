using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Common
{
    public interface IBaseRelationalService<TEntity, Ttype> where TEntity : BaseEntity<Ttype>
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task SaveAsync(CancellationToken cancellationToken);

        Task<TEntity?> GetByIdAsync(object id);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate);
    }
}

using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.Common;
using Ambev.DeveloperEvaluation.ORM.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories.Common
{
    public class BaseRelationalDatabaseRepository<TEntity>: IBaseRelationalDatabaseRepository<TEntity> where TEntity : class
    {
        protected readonly PostgreContext _context;

        protected BaseRelationalDatabaseRepository(PostgreContext context)
        {
            _context = context;
        }

        public void Insert(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _context.FindAsync<TEntity>(id);
        }

        public async Task<int> CountAsync(Expression< Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>().AsNoTracking().CountAsync(predicate, cancellationToken);
        }

        public IEnumerable<TEntity> GetAllAsync(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().AsNoTracking().Where(predicate);
        }
    }
}

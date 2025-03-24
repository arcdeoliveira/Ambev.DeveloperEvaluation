using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.Common;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Common;

namespace Ambev.DeveloperEvaluation.Domain.DomainServices.Common
{
    public class BaseRelationalService<TEntity, Ttype> : IBaseRelationalService<TEntity, Ttype> where TEntity : BaseEntity<Ttype>
    {
        private readonly IBaseRelationalDatabaseRepository<TEntity> _baseRepository;

        protected BaseRelationalService(IBaseRelationalDatabaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return  await _baseRepository.CountAsync(predicate, cancellationToken);    
        }

        public void Delete(TEntity entity)
        {
            _baseRepository.Delete(entity);
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool> predicate)
        {
            return _baseRepository.GetAll(predicate);  
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _baseRepository.GetByIdAsync(id);    
        }

        public void Insert(TEntity entity)
        {
            _baseRepository.Insert(entity);
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await _baseRepository.SaveAsync(cancellationToken);
        }

        public void Update(TEntity entity)
        {
            _baseRepository.Update(entity); 
        }
    }
}

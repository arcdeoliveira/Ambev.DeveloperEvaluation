using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.Common;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.Context;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.ORM.Repositories.Common
{
    public class BaseMongoDBRepository<TDocument> : IBaseMongoDBRepository<TDocument> where TDocument : BaseMongoDBEntity
    {
        protected readonly IMongoCollection<TDocument> _collection;

        protected BaseMongoDBRepository(IMongoDbContext context, string collectionName)
        {
            _collection = context.GetCollection<TDocument>(collectionName);
        }

        public async Task CreateAsync(TDocument entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TDocument>> FindAsync(Expression<Func<TDocument, bool>> filter)
        {
            return (IEnumerable<TDocument>)await _collection.FindAsync(filter);
        }

        public Task<IEnumerable<TDocument>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<TDocument> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<(IEnumerable<TDocument> Items, long TotalCount)> GetPagedAsync(Expression<Func<TDocument, bool>> filter, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(string id, TDocument entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == id, entity);
        }
    }
}

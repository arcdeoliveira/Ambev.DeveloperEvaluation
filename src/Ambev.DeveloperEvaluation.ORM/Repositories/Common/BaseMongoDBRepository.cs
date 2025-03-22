using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Context;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.Common;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.ORM.Repositories.Common
{
    public class BaseMongoDBRepository<TDocument> : IBaseMongoDBRepository<TDocument> where TDocument : BaseDocument
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

        public async Task<long> DocumentExist(string id, CancellationToken cancellationToken)
        {
            return await _collection.CountDocumentsAsync(c => c.Id == id, null, cancellationToken);
        }

        public async Task<long> CountDocuments(FilterDefinition<TDocument> filter, CancellationToken cancellationToken)
        {
            return await _collection.CountDocumentsAsync(filter, null, cancellationToken);
        }

        public async Task<IEnumerable<TDocument>> FindAsync(Expression<Func<TDocument, bool>> filter)
        {
            return (IEnumerable<TDocument>)await _collection.FindAsync(filter);
        }

        public async Task<TDocument> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, TDocument entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == id, entity);
        }
    }
}
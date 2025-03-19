using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.Context;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.ORM.Contexts
{
    public class MongoDBContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionURI);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}

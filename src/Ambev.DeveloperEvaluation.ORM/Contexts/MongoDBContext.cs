using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Context;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.ORM.Contexts
{
    public class MongoDBContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IOptions<MongoDbSettings> options)
        {
            var settings = options.Value;   

            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}

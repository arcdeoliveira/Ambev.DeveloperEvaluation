using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Context
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ambev.DeveloperEvaluation.Domain.Common
{
    public abstract class BaseDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = string.Empty;

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        [BsonElement("UpdatedAt")]
        public DateTime? UpdatedAt { get; private set; }


        public void AlterDateUpdate()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}

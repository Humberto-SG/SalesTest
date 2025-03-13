using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SalesTest.src.Entities
{
    public class SaleEventLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid SaleId { get; set; }

        public string EventType { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}

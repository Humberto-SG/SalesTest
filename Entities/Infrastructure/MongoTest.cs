using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class MongoTest
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Message { get; set; } = string.Empty;
}

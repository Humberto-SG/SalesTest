using MongoDB.Driver;

public class MongoDbService
{
    private readonly IMongoDatabase _database;

    public MongoDbService(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["MongoSettings:ConnectionString"]);
        _database = client.GetDatabase(configuration["MongoSettings:DatabaseName"]);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}

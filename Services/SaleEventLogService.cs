using MongoDB.Driver;
using SalesTest.Entities;

public class SaleEventLogService
{
    private readonly IMongoCollection<SaleEventLog> _eventLogs;

    public SaleEventLogService(MongoDbService mongoDbService)
    {
        _eventLogs = mongoDbService.GetCollection<SaleEventLog>("SaleEventLogs");
    }

    public async Task AddEventAsync(Guid saleId, string eventType)
    {
        var saleEvent = new SaleEventLog
        {
            SaleId = saleId,
            EventType = eventType
        };

        await _eventLogs.InsertOneAsync(saleEvent);
    }

    public async Task<List<SaleEventLog>> GetEventsBySaleIdAsync(Guid saleId)
    {
        return await _eventLogs.Find(e => e.SaleId == saleId).ToListAsync();
    }
}

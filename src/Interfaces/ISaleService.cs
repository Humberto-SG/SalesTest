using SalesTest.src.Entities;

public interface ISaleService
{
    Task<IEnumerable<Sale>> GetAllSalesAsync();
    Task<Sale?> GetSaleByIdAsync(Guid id);
    Task<Sale> CreateSaleAsync(Sale sale);
    Task<Sale?> UpdateSaleAsync(Guid id, Sale updatedSale);
    Task<bool> DeleteSaleAsync(Guid id);
}

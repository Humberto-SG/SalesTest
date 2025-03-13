using Microsoft.EntityFrameworkCore;
using SalesTest.src.Entities;


namespace SalesTest.src.Services
{
    public class SaleService : ISaleService
    {
        private readonly SaleDbContext _context;
        private readonly SaleEventLogService _eventLogService;

        public SaleService(SaleDbContext context, SaleEventLogService eventLogService)
        {
            _context = context;
            _eventLogService = eventLogService;
        }

        public async Task<Sale> CreateSaleAsync(Sale sale)
        {
            sale.CalculateTotal();
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
            await _eventLogService.AddEventAsync(sale.Id, "SaleCreated");
            Console.WriteLine($"Evento: SaleCreated - {sale.SaleNumber}");
            return sale;
        }

        public async Task<IEnumerable<Sale>> GetFilteredSalesAsync(DateTime? startDate, DateTime? endDate, string? customer, string? branch, int page = 1, int pageSize = 10)
        {
            var query = _context.Sales.Include(s => s.Items).AsQueryable();

            if (startDate.HasValue)
                query = query.Where(s => s.SaleDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(s => s.SaleDate <= endDate.Value);

            if (!string.IsNullOrEmpty(customer))
                query = query.Where(s => s.Customer.Contains(customer));

            if (!string.IsNullOrEmpty(branch))
                query = query.Where(s => s.Branch.Contains(branch));

            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Sale?> CancelSaleItemAsync(Guid saleId, int productId)
        {
            var sale = await GetSaleByIdAsync(saleId);
            if (sale == null) return null;

            var item = sale.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null) return null;

            sale.Items.Remove(item);
            sale.CalculateTotal();
            await _context.SaveChangesAsync();

            Console.WriteLine($"Evento: ItemCancelled - Produto {productId} na Venda {sale.SaleNumber}");
            return sale;
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync() => await _context.Sales.Include(s => s.Items).ToListAsync();

        public async Task<Sale?> GetSaleByIdAsync(Guid id) => await _context.Sales.Include(s => s.Items).FirstOrDefaultAsync(s => s.Id == id);

        public async Task<Sale?> UpdateSaleAsync(Guid id, Sale updatedSale)
        {
            var existingSale = await GetSaleByIdAsync(id);
            if (existingSale == null) return null;

            existingSale.Customer = updatedSale.Customer;
            existingSale.Branch = updatedSale.Branch;
            existingSale.Items = updatedSale.Items;
            existingSale.TotalAmount = updatedSale.TotalAmount;
            existingSale.CalculateTotal();

            Sale result = _context.Sales.Update(existingSale).Entity;
            await _context.SaveChangesAsync();
            
            Console.WriteLine($"Evento: SaleModified - {existingSale.SaleNumber}");
            return existingSale;
        }

        public async Task<bool> DeleteSaleAsync(Guid id)
        {
            var sale = await GetSaleByIdAsync(id);
            if (sale == null) return false;

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Evento: SaleCancelled - {sale.SaleNumber}");
            return true;
        }

    }

}
using SalesTest.src.Entities;

namespace SalesTest.src.Repositories
{
    public class SaleRepository
    {
        private static readonly List<Sale> Sales = new();

        public void Add(Sale sale) => Sales.Add(sale);
        public List<Sale> GetAll() => Sales;
        public Sale GetById(Guid id) => Sales.FirstOrDefault(s => s.Id == id);
        public void Update(Sale sale)
        {
            var index = Sales.FindIndex(s => s.Id == sale.Id);
            if (index >= 0) Sales[index] = sale;
        }
    }
}

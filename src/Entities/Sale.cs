using System;
using System.Collections.Generic;

namespace SalesTest.src.Entities
{
    public class Sale
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public string Customer { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Branch { get; set; } = string.Empty;
        public List<SaleItem> Items { get; set; } = new();
        public bool IsCancelled { get; set; } = false;

        public void CalculateTotal()
        {
            TotalAmount = 0;
            foreach (var item in Items)
            {
                item.ApplyDiscount();
                TotalAmount += item.Total;
            }
        }
    }

    public class SaleItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; private set; }
        public decimal Total => UnitPrice * Quantity - Discount;

        public void ApplyDiscount()
        {
            if (Quantity >= 4 && Quantity <= 9)
                Discount = UnitPrice * Quantity * 0.10m;
            else if (Quantity >= 10 && Quantity <= 20)
                Discount = UnitPrice * Quantity * 0.20m;
            else
                Discount = 0;

            if (Quantity > 20)
                throw new InvalidOperationException("Não é possível vender mais de 20 itens por produto.");
        }
    }
}
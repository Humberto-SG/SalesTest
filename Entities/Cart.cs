
using System.ComponentModel.DataAnnotations;

namespace SalesTest.Entities
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }
        public List<CartItem> Items { get; set; } = new();

        public decimal TotalAmount { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public void CalculateTotal()
        {
            TotalAmount = 0;
            foreach (var item in Items)
            {
                item.TotalPrice = item.UnitPrice * item.Quantity;
                TotalAmount += item.TotalPrice;
            }
        }
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

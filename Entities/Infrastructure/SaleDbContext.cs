using Microsoft.EntityFrameworkCore;
using SalesTest.Entities;

public class SaleDbContext : DbContext
{
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleEventLog> SaleEventLogs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Cart> Carts { get; set; }

    public SaleDbContext(DbContextOptions<SaleDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sale>().OwnsMany(s => s.Items);
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Cart>().OwnsMany(c => c.Items);
    }
}
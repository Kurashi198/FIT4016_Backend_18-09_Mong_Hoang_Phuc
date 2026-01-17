using Microsoft.EntityFrameworkCore;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Name).IsUnique();

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Sku).IsUnique();

        modelBuilder.Entity<Order>()
            .HasIndex(o => o.OrderNumber).IsUnique();

        modelBuilder.Entity<Order>()
            .HasIndex(o => o.CustomerEmail).IsUnique();

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Product)
            .WithMany(p => p.Orders)
            .HasForeignKey(o => o.ProductId);
    }
}

using Microsoft.EntityFrameworkCore;
using OrderKeeper.Database.Order;
using OrderKeeper.Database.OrderItem;
using OrderKeeper.Database.Provider;

namespace OrderKeeper.Database;

public class PostgresContext : DbContext
{
    public DbSet<ProviderModel> Provider { get; set; }
    public DbSet<OrderModel> Order { get; set; }
    public DbSet<OrderItemModel> OrderItem { get; set; }

    
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) { }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}
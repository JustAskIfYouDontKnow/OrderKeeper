using Microsoft.EntityFrameworkCore;
using OrderKeeper.Database.Order;
using OrderKeeper.Database.Provider;

namespace OrderKeeper.Database;

public class PostgresContext : DbContext
{
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) { }
    
    public DbSet<ProviderModel> Provider { get; set; }
    public DbSet<OrderModel> Order { get; set; }
    public DbSet<OrderModel> OrderItem { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}
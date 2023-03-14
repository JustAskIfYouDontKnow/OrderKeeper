using Microsoft.EntityFrameworkCore;

namespace OrderKeeper.Database;

public class PostgresContext : DbContext
{
    public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}
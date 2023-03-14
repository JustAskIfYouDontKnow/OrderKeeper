using OrderKeeper.Database.Order;
using OrderKeeper.Database.OrderItem;
using OrderKeeper.Database.Provider;

namespace OrderKeeper.Database;

public class DatabaseContainer : IDatabaseContainer
{

    public IProviderRepository Provider { get; set; }

    public IOrderRepository Order { get; set; }

    public IOrderItemRepository OrderItem { get; set; }


    public DatabaseContainer(PostgresContext db)
    {
        Provider = new ProviderRepository(db);
        Order = new OrderRepository(db);
        OrderItem = new OrderItemRepository(db);
    }

}
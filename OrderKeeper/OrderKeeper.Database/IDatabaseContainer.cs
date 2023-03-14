using OrderKeeper.Database.Order;
using OrderKeeper.Database.OrderItem;
using OrderKeeper.Database.Provider;

namespace OrderKeeper.Database;

public interface IDatabaseContainer
{
    IProviderRepository Provider { get; set; }
    IOrderRepository Order { get; set; }
    IOrderItemRepository OrderItem { get; set; }
}
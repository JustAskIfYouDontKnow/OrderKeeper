using OrderKeeper.Database.Service.Order;

namespace OrderKeeper.Database.Service;

public interface IServiceContainer
{
    IOrderService Order { get; }
}
using OrderKeeper.Database.Service.Order;

namespace OrderKeeper.Database.Service;

public class ServiceContainer : IServiceContainer
{

    public IOrderService Order { get; set; }


    public ServiceContainer(IOrderService order)
    {
        Order = order;
    }


}
using OrderKeeper.Database;

namespace OrderKeeper.API.Controllers.Client;

public class OrderItemController : AbstractClientController
{

    public OrderItemController(IDatabaseContainer databaseContainer) : base(databaseContainer) { }
    
}
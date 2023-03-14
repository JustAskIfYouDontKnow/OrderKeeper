using OrderKeeper.Database;

namespace OrderKeeper.API.Controllers.Client;

public class OrderController : AbstractClientController
{

    public OrderController(IDatabaseContainer databaseContainer) : base(databaseContainer) { }
    
}
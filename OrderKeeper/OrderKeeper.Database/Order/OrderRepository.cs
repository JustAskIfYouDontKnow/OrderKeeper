namespace OrderKeeper.Database.Order;

public class OrderRepository : AbstractRepository<OrderModel>, IOrderRepository
{

    public OrderRepository(PostgresContext context) : base(context) { }

}
namespace OrderKeeper.Database.OrderItem;

public class OrderItemRepository : AbstractRepository<OrderItemModel>, IOrderItemRepository
{

    public OrderItemRepository(PostgresContext context) : base(context) { }
    
}

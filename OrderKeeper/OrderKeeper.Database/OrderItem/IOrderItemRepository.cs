
namespace OrderKeeper.Database.OrderItem;

public interface IOrderItemRepository
{
    Task<OrderItemModel> GetOne(int id);

    Task<OrderItemModel> CreateItem(int orderId, string name, decimal quantity, string unit);

    Task<bool> DeleteOrderItemFromOrder(OrderItemModel orderItem);

    Task<List<OrderItemModel>> ListByProviderId(int providerId);

}
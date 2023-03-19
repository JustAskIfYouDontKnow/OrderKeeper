using OrderKeeper.Model.Order;
using OrderKeeper.Model.OrderItem;

namespace OrderKeeper.Database.Order;

public interface IOrderRepository
{
    Task<OrderModel> GetOne(int id);
    
    Task<OrderModel> CreateOrder(string number, int providerId);

    Task<OrderModel> UpdateOrder(OrderModel model, OrderPatch patch, List<OrderItemPatch>? orderItemsPatch = null);

    Task<List<OrderModel>> GetListOrdersByDateRange(DateTime startDateTime, DateTime endDateTime, List<int> providerId, string? sortBy);
    
    Task <List<OrderModel>> GetListByProviderId(int providerId);
    
    Task<List<OrderModel>> GetListAllOrders();
}
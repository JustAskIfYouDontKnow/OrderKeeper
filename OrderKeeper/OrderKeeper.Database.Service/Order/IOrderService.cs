using OrderKeeper.Client.Order;

namespace OrderKeeper.Database.Service.Order;

public interface IOrderService
{
    Task<List<Client.Payload.Order.Order>> ListOrdersByDateRange(DateTime startDateTime, DateTime endDateTime, List<int> providerId);


    Task<List<Client.Payload.Order.Order>> ListOrdersByDateRangeWithSort(
        DateTime startDateTime,
        DateTime endDateTime,
        List<int> providerId,
        string sortBy
    );


    Task<List<Client.Payload.Order.Order>> ListAllOrders();

    Task<GetOneOrder.Response> GetOrder(int orderId);

}
using OrderKeeper.Client.Order;

namespace OrderKeeper.Database.Service.Order;

public class OrderService : IOrderService
{
    
    private readonly IDatabaseContainer _databaseContainer;

    public OrderService(IDatabaseContainer databaseContainer)
    {
        _databaseContainer = databaseContainer;
    }
    
    public async Task<List<Client.Payload.Order.Order>> ListOrdersByDateRange(DateTime startDateTime, DateTime endDateTime, List<int> providerId)
    {
        var orders = await _databaseContainer.Order.ListOrdersByDateRange(startDateTime, endDateTime, providerId, sortBy:null);
        return orders.Select(order => new Client.Payload.Order.Order
        {
            Id = order.Id,
            Number = order.Number,
            Date = order.Date.ToString("g"),
            Provider = new Client.Payload.Provider.Provider()
            {
                Id = order.Provider.Id, 
                Name = order.Provider.Name
            },
            OrderItem = order.OrderItems.Select(
                oi => new Client.Payload.OrderItem.OrderItem()
                {
                    Id = oi.Id,
                    Name = oi.Name,
                    Quantity = oi.Quantity,
                    Unit = oi.Unit
                }
            ).ToList()
        }).ToList();
    }


    public async Task<List<Client.Payload.Order.Order>> ListOrdersByDateRangeWithSort(DateTime startDateTime, DateTime endDateTime, List<int> providerId, string sortBy)
    {
        var orders = await _databaseContainer.Order.ListOrdersByDateRange(startDateTime, endDateTime, providerId, sortBy);
        return orders.Select(order => new Client.Payload.Order.Order
        {
            Id = order.Id,
            Number = order.Number,
            Date = order.Date.ToString("g"),
            Provider = new Client.Payload.Provider.Provider()
            {
                Id = order.Provider.Id, 
                Name = order.Provider.Name
            },
            OrderItem = order.OrderItems.Select(
                oi => new Client.Payload.OrderItem.OrderItem()
                {
                    Id = oi.Id,
                    Name = oi.Name,
                    Quantity = oi.Quantity,
                    Unit = oi.Unit
                }
            ).ToList()
        }).ToList();
    }


    public async Task<List<Client.Payload.Order.Order>> ListAllOrders()
    {
        var orders = await _databaseContainer.Order.ListAllOrders();
        
        return orders.Select(order => new Client.Payload.Order.Order
        {
            Id = order.Id,
            Number = order.Number,
            Date = order.Date.ToString("g"),
            Provider = new Client.Payload.Provider.Provider()
            {
                Id = order.Provider.Id, 
                Name = order.Provider.Name
            },
            OrderItem = order.OrderItems.Select(
                oi => new Client.Payload.OrderItem.OrderItem()
                {
                    Id = oi.Id,
                    Name = oi.Name,
                    Quantity = oi.Quantity,
                    Unit = oi.Unit
                }
            ).ToList()
        }).ToList();
    }


    public async Task<GetOneOrder.Response> GetOrder(int orderId)
    {
        var order = await _databaseContainer.Order.GetOne(orderId);

        return new GetOneOrder.Response()
        {
            Order = new Client.Payload.Order.Order
            {
                Id = order.Id,
                Number = order.Number,
                Date = order.Date.ToString("g"),
                Provider = new Client.Payload.Provider.Provider()
                {
                    Id = order.Provider.Id, 
                    Name = order.Provider.Name
                },
                OrderItem = order.OrderItems.Select(
                    oi => new Client.Payload.OrderItem.OrderItem()
                    {
                        Id = oi.Id,
                        Name = oi.Name,
                        Quantity = oi.Quantity,
                        Unit = oi.Unit
                    }
                ).ToList()
            }
        };
    }

}
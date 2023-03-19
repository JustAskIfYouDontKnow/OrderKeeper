using Microsoft.AspNetCore.Mvc;
using OrderKeeper.Client.Order;
using OrderKeeper.Client.OrderItem;
using OrderKeeper.Client.Payload.Order;
using OrderKeeper.Client.Payload.OrderItem;
using OrderKeeper.Client.Payload.Provider;
using OrderKeeper.Database;
using OrderKeeper.Model.Order;
using OrderKeeper.Model.OrderItem;

namespace OrderKeeper.API.Controllers.Client;

public class OrderController : AbstractClientController
{

    public OrderController(IDatabaseContainer databaseContainer) : base(databaseContainer) { }


    [HttpGet]
    public async Task<GetOneOrder.Response> GetOrder(int orderId)
    {
        var order = await DatabaseContainer.Order.GetOne(orderId);

        return new GetOneOrder.Response()
        {
            Order = new Order
            {
                Id = order.Id,
                Number = order.Number,
                Date = order.Date,
                Provider = new Provider()
                {
                    Id = order.Provider.Id, 
                    Name = order.Provider.Name
                },
                OrderItem = order.OrderItems.Select(
                        oi => new OrderItem()
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


    [HttpPost]
    public async Task<IActionResult> CreateOrder(string orderNumber, int providerId, [FromBody] List<OrderItemDetail> items)
    {
        if (items.Count == 0)
        {
            return BadRequest(new {message = "Заказ не может быть пустым."});
        }

        if (await IsProductNameSameAsOrderNumber(orderNumber, items))
        {
            return BadRequest(new {message = "Имя продукта не может совпадать с именем заказа"});
        }

        if (await IsSingleOrderNumberByProviderId(orderNumber,providerId))
        {
            return BadRequest(new {message = "Такой номер заказа уже существует"});
        }

        var order = await DatabaseContainer.Order.CreateOrder(orderNumber, providerId);

        foreach (var item in items)
        {
            await DatabaseContainer.OrderItem.CreateItem(order.Id, item.Name, item.Quantity, item.Unit);
        }

        return Ok(order);
    }


    [HttpPost]
    public async Task<IActionResult> AddItemToOrder(int orderId, [FromBody] List<OrderItemDetail> request)
    {
        var order = await DatabaseContainer.Order.GetOne(orderId);

        if (await IsProductNameSameAsOrderNumber(order.Number, request))
        {
            return BadRequest(new {message = "Имя продукта не может совпадать с именем заказа"});
        }

        foreach (var item in request)
        {
            await DatabaseContainer.OrderItem.CreateItem(order.Id, item.Name, item.Quantity, item.Unit);
        }

        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> DeleteItemInOrder(List<int> itemIds)
    {
        foreach (var id in itemIds)
        {
            var orderItem = await DatabaseContainer.OrderItem.GetOne(id);
            await DatabaseContainer.OrderItem.DeleteOrderItemFromOrder(orderItem);
        }

        return Ok();
    }


    [HttpPut]
    public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrder request)
    {
        
        var order = await DatabaseContainer.Order.GetOne(request.OrderId);

        var orderPatch = new OrderPatch(request.ProviderId, request.Number);

        var orderItemsPatch = request.OrderItems?.Select(x => new OrderItemPatch(x.Id, x.Name, x.Quantity, x.Unit)).ToList();

        if (orderItemsPatch?.Any(item => item.Name == request.Number) == true)
        {
            return BadRequest(new {message = "Имя продукта не может совпадать с именем заказа"});
        }
        
        if (await IsSingleOrderNumberByProviderId(request.Number, request.ProviderId))
        {
            return BadRequest(new {message = "Такой номер заказа уже существует"});
        }

        await DatabaseContainer.Order.UpdateOrder(order, orderPatch, orderItemsPatch);

        return Ok();

    }


    [HttpPost]
    public async Task<IActionResult> ListOrdersByDateRange(DateTime startDateTime, DateTime endDateTime, List<int> providerId)
    {
        var utcStartDateTime = startDateTime.ToUniversalTime();
        var utcEndDateTime = endDateTime.ToUniversalTime();
        var orders = await DatabaseContainer.Order.GetListOrdersByDateRange(utcStartDateTime, utcEndDateTime, providerId, sortBy:null);
        return Ok(orders);
    }


    [HttpPost]
    public async Task<IActionResult> ListOrdersByDateRangeWithSort(DateTime startDateTime, DateTime endDateTime, List<int> providerId, string sortBy)
    {
        var utcStartDateTime = startDateTime.ToUniversalTime();
        var utcEndDateTime = endDateTime.ToUniversalTime();
        var orders = await DatabaseContainer.Order.GetListOrdersByDateRange(utcStartDateTime, utcEndDateTime, providerId, sortBy);

        return Ok(orders);
    }


    [HttpGet]
    public async Task<IActionResult> ListAllOrders()
    {
        var orders = await DatabaseContainer.Order.GetListAllOrders();
        return Ok(orders);
    }


    private async Task<bool> IsProductNameSameAsOrderNumber(string orderNumber, IEnumerable<OrderItemDetail> items)
    {
        return items.Any(item => item.Name == orderNumber);
    }


    private async Task<bool> IsSingleOrderNumberByProviderId(string number, int providerId)
    {
        var orders = await DatabaseContainer.Order.GetListByProviderId(providerId);
        return orders.Any(item => item.Number == number);
    }

}
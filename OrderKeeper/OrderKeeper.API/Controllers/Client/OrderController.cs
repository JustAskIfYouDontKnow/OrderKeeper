using Microsoft.AspNetCore.Mvc;
using OrderKeeper.Client.Order;
using OrderKeeper.Client.OrderItem;
using OrderKeeper.Client.Payload.Order;
using OrderKeeper.Database;
using OrderKeeper.Database.Service.Order;
using OrderKeeper.Model.Order;
using OrderKeeper.Model.OrderItem;

namespace OrderKeeper.API.Controllers.Client;

public class OrderController : AbstractClientController
{
    private readonly IOrderService _orderService;

    public OrderController(IDatabaseContainer databaseContainer, IOrderService orderService) : base(databaseContainer)
    {
        _orderService = orderService;
    }
    

    [HttpGet]
    public async Task<GetOneOrder.Response> GetOrder(int orderId)
    {
        return await _orderService.GetOrder(orderId);
    }


    [HttpPost]
    public async Task<IActionResult> CreateOrder(string orderNumber, int providerId, [FromBody] List<OrderItemDetail> items)
    {
        if (items.Count == 0)
        {
            return BadRequest(new {message = "Заказ не может быть пустым."});
        }

        if (items.Any(item => item.Name == orderNumber))
        {
            return BadRequest(new {message = "Имя продукта не может совпадать с именем заказа"});
        }

        
        var isExistOrder = DatabaseContainer.Order.IsExistOrderByProviderId(providerId, orderNumber);
        if (isExistOrder)
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


    [HttpDelete]
    public async Task<IActionResult> DeleteOrderById(int orderId)
    {
        var order = await DatabaseContainer.Order.GetOne(orderId);
        await DatabaseContainer.Order.Delete(order);
        return Ok();
    }

    
    [HttpPost]
    public async Task<IActionResult> AddItemToOrder(int orderId, [FromBody] List<OrderItemDetail> request)
    {
        var order = await DatabaseContainer.Order.GetOne(orderId);

        if (request.Any(item => item.Name == order.Number))
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
        var orderItemsPatch = request.OrderItems.Select(x => new OrderItemPatch(x.Id, x.Name, x.Quantity, x.Unit)).ToList();
        var existOrderItem = await DatabaseContainer.OrderItem.ListByProviderId(request.ProviderId);

        if (existOrderItem.Any(item => item.Name == request.Number) || orderItemsPatch.Any(item => item.Name == request.Number))
        {
            return BadRequest(new {message = "Имя продукта не может совпадать с именем заказа"});
        }

        if (request.ProviderId != order.ProviderId || request.Number != order.Number)
        {
            var isExistOrder = DatabaseContainer.Order.IsExistOrderByProviderId(request.ProviderId, request.Number);

            if (isExistOrder)
            {
                return BadRequest(new {message = "Такой номер заказа уже существует"});
            }
        }

        await DatabaseContainer.Order.UpdateOrder(order, orderPatch, orderItemsPatch);

        return Ok();

    }


    [HttpPost]
    public async Task<List<Order>> ListOrdersByDateRange(DateTime startDateTime, DateTime endDateTime, List<int> providerId)
    {
        return await _orderService.ListOrdersByDateRange(startDateTime, endDateTime, providerId);
    }


    [HttpPost]
    public async Task<List<Order>> ListOrdersByDateRangeWithSort(DateTime startDateTime, DateTime endDateTime, List<int> providerId, string sortBy)
    {
        return await _orderService.ListOrdersByDateRangeWithSort(startDateTime, endDateTime, providerId, sortBy);
    }


    [HttpGet]
    public async Task<List<Order>> ListAllOrders()
    {
        return await _orderService.ListAllOrders();
    }
}
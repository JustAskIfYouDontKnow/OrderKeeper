using Microsoft.EntityFrameworkCore;

namespace OrderKeeper.Database.OrderItem;

public class OrderItemRepository : AbstractRepository<OrderItemModel>, IOrderItemRepository
{

    public OrderItemRepository(PostgresContext context) : base(context) { }

   public async Task<OrderItemModel> GetOne(int id)
    {
        var orderItem = await DbModel
            .FirstOrDefaultAsync(x => x.Id == id);

        if (orderItem == null)
        {
            throw new Exception("Order model is not found.");
        }

        return orderItem;
    }


    public async Task<OrderItemModel> CreateItem(int orderId, string name, decimal quantity, string unit)
    {
        var model = OrderItemModel.CreateModel(orderId, name, quantity, unit);
        
        var result = await CreateModelAsync(model);

        if (result == null)
        {
            throw new Exception("OrderItem model is not created.");
        }

        return result;
    }
    
    public async Task<bool> DeleteOrderItemFromOrder(OrderItemModel orderItem)
    {
        await DeleteModel(orderItem);
        return true;
    }
}

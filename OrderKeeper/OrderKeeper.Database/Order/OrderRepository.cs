using Microsoft.EntityFrameworkCore;
using OrderKeeper.Model.Order;
using OrderKeeper.Model.OrderItem;

namespace OrderKeeper.Database.Order;

public class OrderRepository : AbstractRepository<OrderModel>, IOrderRepository
{

    public OrderRepository(PostgresContext context) : base(context) { }


    public async Task<OrderModel> GetOne(int id)
    {
        var provider = await DbModel.Include(x => x.OrderItems).Include(x => x.Provider).FirstOrDefaultAsync(x => x.Id == id);

        if (provider == null)
        {
            throw new Exception("Order model is not found.");
        }

        return provider;
    }


    public async Task<OrderModel> CreateOrder(string number, int providerId)
    {
        var model = OrderModel.CreateModel(number, providerId);

        var result = await CreateModelAsync(model);

        if (result == null)
        {
            throw new Exception("OrderModel is not created.");
        }

        return result;
    }


    public bool IsExistOrderByProviderId(int providerId, string number)
    {
        return DbModel.Where(x => x.ProviderId == providerId).Any(x => x.Number == number);
    }

    
    
    public async Task<bool> Delete(OrderModel order)
    {
        await DeleteModel(order);
        return true;
    }



    public async Task<OrderModel> UpdateOrder(OrderModel model, OrderPatch patch, List<OrderItemPatch>? orderItemsPatch = null)
    {
        if (model.IsSame(patch) && (orderItemsPatch == null || orderItemsPatch.Count == 0))
        {
            throw new Exception("Order is not updated. Data is the same");
        }

        if (patch.Number is not null)
        {
            model.UpdateByPatch(patch);
        }
        
        
        foreach (var itemPatch in orderItemsPatch)
        {
            var item = model.OrderItems.FirstOrDefault(x => x.Id == itemPatch.Id);

            if (item == null)
            {
                throw new Exception($"Order item with id={itemPatch.Id} not found.");
            }

            item.UpdateByPatch(itemPatch);
        }
    

        var result = await UpdateModelAsync(model);

        if (result == 0)
        {
            throw new Exception("Order is not updated. Database error.");
        }

        return model;
    }


    public async Task<List<OrderModel>> ListOrdersByDateRange(
        DateTime startDateTime,
        DateTime endDateTime,
        List<int> providerId,
        string? sortBy
    )
    {
        var orders = await DbModel
            .Include(x => x.Provider)
            .Include(x=> x.OrderItems)
            .Where(o => o.Date >= startDateTime && o.Date <= endDateTime && providerId.Contains(o.ProviderId))
            .ToListAsync();

        switch (sortBy)
        {
            case "number_asc":
                orders = orders.OrderBy(o => o.Number).ToList();
                break;

            case "number_desc":
                orders = orders.OrderByDescending(o => o.Number).ToList();
                break;

            case "date_asc":
                orders = orders.OrderBy(o => o.Date).ToList();
                break;

            case "date_desc":
                orders = orders.OrderByDescending(o => o.Date).ToList();
                break;

            case "provider_asc":
                orders = orders.OrderBy(o => o.ProviderId).ToList();
                break;

            case "provider_desc":
                orders = orders.OrderByDescending(o => o.ProviderId).ToList();
                break;

            default:
                break;

        }

        return orders;
    }


    public async Task<List<OrderModel>> ListByProviderId(int providerId)
    {
        return await DbModel.Include(x => x.OrderItems).Where(x => x.Provider.Id == providerId).ToListAsync();
    }
    

    public async Task<List<OrderModel>> ListAllOrders()
    {
        return await DbModel.Include(x => x.OrderItems).Include(x => x.Provider).ToListAsync();
    }
}
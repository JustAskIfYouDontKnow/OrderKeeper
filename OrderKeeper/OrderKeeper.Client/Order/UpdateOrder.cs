using System.ComponentModel.DataAnnotations;
using OrderKeeper.Model.OrderItem;

namespace OrderKeeper.Client.Order;

public class UpdateOrder
{
    [Required]
    public int OrderId { get; set; }

    [Required]
    public int ProviderId { get; set; }

    [Required]
    public string Number { get; set; }
    
    [Required]
    public List<OrderItemPatch> OrderItems { get; set; }
}
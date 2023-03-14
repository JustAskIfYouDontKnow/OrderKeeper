using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrderKeeper.Database.Order;

namespace OrderKeeper.Database.OrderItem;

public class OrderItemModel : AbstractModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    
    [Required]
    public OrderModel Order { get; set; }

    [Required]
    [MaxLength]
    public string Name { get; set; }

    [Required]
    public decimal Quantity { get; set; }

    [Required]
    [MaxLength]
    public string Unit { get; set; }
    

    public static OrderItemModel CreateModel(int orderId, string name, decimal quantity, string unit, OrderModel order)
    {
        return new OrderItemModel
        {
            OrderId = orderId,
            Name = name,
            Quantity = quantity,
            Unit = unit,
            Order = order,
        };
    }
}
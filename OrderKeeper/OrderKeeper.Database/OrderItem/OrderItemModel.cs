using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using OrderKeeper.Database.Order;
using OrderKeeper.Model.OrderItem;

namespace OrderKeeper.Database.OrderItem;

public class OrderItemModel : AbstractModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Order")]
    public int OrderId { get; set; }


    [JsonIgnore]
    public OrderModel Order { get; set; }

    [Required]
    [MaxLength]
    public string Name { get; set; }

    [Required]
    public decimal Quantity { get; set; }

    [Required]
    [MaxLength]
    public string Unit { get; set; }


    public static OrderItemModel CreateModel(int orderId, string name, decimal quantity, string unit)
    {
        return new OrderItemModel
        {
            OrderId = orderId, Name = name, Quantity = quantity, Unit = unit,
        };
    }


    public bool IsSame(OrderItemPatch patch)
    {
        return patch.Name == Name && patch.Unit == Unit && patch.Quantity == Quantity;
    }


    public void UpdateByPatch(OrderItemPatch patch)
    {
        Name = patch.Name;
        Unit = patch.Unit;
        Quantity = patch.Quantity;
    }
}
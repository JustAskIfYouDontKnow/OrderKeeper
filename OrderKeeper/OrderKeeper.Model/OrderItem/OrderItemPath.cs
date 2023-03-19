using System.ComponentModel.DataAnnotations;

namespace OrderKeeper.Model.OrderItem;

public class OrderItemPatch
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Quantity { get; set; }

    [Required]
    public string Unit { get; set; }


    public OrderItemPatch(int id, string name, decimal quantity, string unit)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        Unit = unit;

    }
}
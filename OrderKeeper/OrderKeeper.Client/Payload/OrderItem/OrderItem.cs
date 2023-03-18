using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderKeeper.Client.Payload.OrderItem;

public class OrderItem
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int OrderId { get; set; }
    
    [Required]
    public Order.Order Order { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Quantity { get; set; }

    [Required]
    public string Unit { get; set; }
}
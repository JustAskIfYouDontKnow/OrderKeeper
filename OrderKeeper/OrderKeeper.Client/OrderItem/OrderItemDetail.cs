using System.ComponentModel.DataAnnotations;

namespace OrderKeeper.Client.OrderItem;

public class OrderItemDetail
{
    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Quantity { get; set; }

    [Required]
    public string Unit { get; set; }
    
    
    public class Response
    {
    }
}
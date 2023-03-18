using System.ComponentModel.DataAnnotations;

namespace OrderKeeper.Client.OrderItem;

public class AddItem
{
    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Quantity { get; set; }

    [Required]
    public string Unit { get; set; }
    
    public class Response
    {
        [Required]
        public Payload.Order.Order Order { get; set; }
    }
}
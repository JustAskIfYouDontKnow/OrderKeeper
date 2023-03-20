using System.ComponentModel.DataAnnotations;

namespace OrderKeeper.Client.Payload.Order;

public class Order
{

    [Required]
    public int Id { get; set; }

    [Required]
    public Provider.Provider Provider { get; set; }

    [Required]
    public string Number { get; set; }
    
    [Required]
    public List<OrderItem.OrderItem> OrderItem { get; set; }

    [Required]
    public string Date { get; set; }
    
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrderKeeper.Database.OrderItem;
using OrderKeeper.Database.Provider;

namespace OrderKeeper.Database.Order;

public class OrderModel : AbstractModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [ForeignKey("Provider")]
    public int ProviderId { get; set; }
    [Required]
    public ProviderModel Provider { get; set; }

    [Required]
    [MaxLength]
    public string Number { get; set; }

    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public List<OrderItemModel> OrderItems { get; set; }


    public static OrderModel CreateModel(string number, int providerId, ProviderModel provider, List<OrderItemModel> orderItems)
    {
        return new OrderModel
        {
            Number = number,
            Date = DateTime.UtcNow,
            ProviderId = providerId,
            Provider = provider,
            OrderItems = orderItems,
        };
    }
}
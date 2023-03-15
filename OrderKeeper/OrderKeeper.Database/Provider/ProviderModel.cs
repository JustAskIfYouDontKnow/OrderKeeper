using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OrderKeeper.Database.Order;

namespace OrderKeeper.Database.Provider;

public class ProviderModel : AbstractModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength]
    public string Name { get; set; }
    
    public List<OrderModel> Orders { get; set; }

    public static ProviderModel CreateModel(string name)
    {
        return new ProviderModel 
        {
            Name = name,
            Orders = new List<OrderModel>()
        };
    }
}
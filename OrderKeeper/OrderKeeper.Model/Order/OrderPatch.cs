using System.ComponentModel.DataAnnotations;

namespace OrderKeeper.Model.Order;

public class OrderPatch
{
    [Required]
    public readonly int ProviderId;

    [Required]
    public readonly string Number;

    public OrderPatch(int providerId, string number)
    {
        ProviderId = providerId;
        Number = number;
    }

}
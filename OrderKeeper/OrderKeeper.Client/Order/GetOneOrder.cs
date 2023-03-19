using System.ComponentModel.DataAnnotations;

namespace OrderKeeper.Client.Order;

public class GetOneOrder
{
    public class Response
    {
        [Required]
        public Payload.Order.Order Order { get; set; }
    }
}
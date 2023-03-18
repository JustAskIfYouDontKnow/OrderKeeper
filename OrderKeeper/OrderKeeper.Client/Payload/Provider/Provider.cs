using System.ComponentModel.DataAnnotations;

namespace OrderKeeper.Client.Payload.Provider;

public class Provider
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}
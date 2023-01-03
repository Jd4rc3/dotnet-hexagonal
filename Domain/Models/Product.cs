using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public record Product
{
    public int Id { get; set; }

    [Required] public string Name { get; set; }

    public int InInventory { get; set; }

    public bool Enabled { get; set; } = true;

    public int Min { get; set; }

    public int Max { get; set; }

    public virtual ICollection<ProductBuy> Buys { get; set; }
}
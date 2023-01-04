namespace Domain.Models;

public class ProductBuy
{
    public int Id { get; set; }

    public Product Product { get; set; }

    public Buy Buy { get; set; }

    public int Quantity { get; set; }
}
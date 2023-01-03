namespace Domain.Models;

public record Buy
{
    public int Id { get; set; }

    public string IdType { get; set; }

    public int IdNumber { get; set; }

    public string ClientName { get; set; }

    public IEnumerable<Product> Products { get; set; }
}
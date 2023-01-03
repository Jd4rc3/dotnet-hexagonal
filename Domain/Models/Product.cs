namespace Domain.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int InInventory { get; set; }

    public bool Enabled { get; set; }

    public int Min { get; set; }

    public int Max { get; set; }
}
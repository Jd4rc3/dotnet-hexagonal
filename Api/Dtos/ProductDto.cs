namespace Api.Dtos;

public class ProductDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int InInventory { get; set; }

    public bool Enabled { get; set; } = true;

    public int Min { get; set; }

    public int Max { get; set; }
}
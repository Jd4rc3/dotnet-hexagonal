namespace Api.Dtos;

public class CreateProductDto
{
    public string Name { get; set; }

    public int Min { get; set; }

    public int Max { get; set; }

    public int InInventory { get; set; }
}
namespace Api.Dtos;

public record PurchaseDto
{
    public string IdType { get; set; }

    public int IdNumber { get; set; }

    public string ClientName { get; set; }

    public virtual ICollection<CreateProductBuyDto> Buys { get; set; }
}
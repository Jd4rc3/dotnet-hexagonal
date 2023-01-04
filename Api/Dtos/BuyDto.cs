using Domain.Models;

namespace Api.Dtos;

public class BuyDto
{
    public int Id { get; set; }

    public string IdType { get; set; }

    public int IdNumber { get; set; }

    public string ClientName { get; set; }

    public virtual ICollection<ProductBuy> Buys { get; set; }
}
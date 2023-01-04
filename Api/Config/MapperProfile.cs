using Api.Dtos;
using AutoMapper;
using Domain.Models;

namespace Api.Config;

public class MapperProfile : Profile
{
    public Func<PurchaseDto, Buy, IEnumerable<ProductBuy>> Map = (productBuyDtos, buy) =>
    {
        return productBuyDtos.Buys.Select(productBuyDto => new ProductBuy
        {
            Product = new Product { Id = productBuyDto.ProductId },
            Quantity = productBuyDto.Quantity
        });
    };

    public MapperProfile()
    {
        CreateMap<CreateProductDto, Product>().ReverseMap();
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<CreateProductBuyDto, ProductBuy>().ReverseMap();

        CreateMap<PurchaseDto, Buy>().ForMember(entity => entity.Buys,
            opt => opt.MapFrom(Map)).ReverseMap();
    }
}
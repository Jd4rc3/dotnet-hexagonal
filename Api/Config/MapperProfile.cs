using Api.Dtos;
using AutoMapper;
using Domain.Models;

namespace Api.Config;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateProductDto, Product>().ReverseMap();
    }
}
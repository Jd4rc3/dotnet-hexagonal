using Api.Config;
using Api.Controllers;
using Api.Dtos;
using AutoMapper;
using Domain.Models;
using Domain.UseCases;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Test.Common;

namespace Test.Api;

public class ProductControllerTest
{
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IMapper _mapper;
    private readonly MockRepository _mockRepository;

    private readonly Mock<UseCase<Task<Product>, Product>> _mockUseCase;

    public ProductControllerTest()
    {
        _mockRepository = new MockRepository(MockBehavior.Default);

        _mockUseCase = _mockRepository.Create<UseCase<Task<Product>, Product>>();

        _configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());

        _mapper = _configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task CreateProduct()
    {
        var product = Builder.GenerateProductsData(1).First();
        var productDto = new CreateProductDto { Max = 200, Min = 80, Name = "Apple", InInventory = 800 };
        ;
        var controller = new ProductController(_mapper);

        _mockUseCase.Setup(useCase => useCase.Apply(new Product { Name = "Anything" })).ReturnsAsync(product);

        var result = await controller.CreateProductAsync(productDto, new[] { _mockUseCase.Object });

        Assert.IsType<OkResult>(result);
    }

    // public void DeleteProduct()
    // {
    //     var product = Builder.GenerateProductsData(1).FirstOrDefault();
    //     var controller = new ProductController(_mapper);
    //
    //     _mockUseCase.Setup(useCase => useCase.Apply(It.IsAny<Product>()))!.ReturnsAsync(product);
    //     
    //    var result = await controller. 
    // }
}
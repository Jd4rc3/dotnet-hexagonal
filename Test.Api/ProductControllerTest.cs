using Api.Config;
using Api.Controllers;
using Api.Dtos;
using AutoMapper;
using Domain.Models;
using Domain.UseCases;
using Microsoft.AspNetCore.Mvc;
using Moq;

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
        var productDto = new CreateProductDto { Max = 200, Min = 80, Name = "Apple", InInventory = 800 };
        var controller = new ProductController(_mockUseCase.Object, _mapper);
        var product = _mapper.Map<Product>(productDto) with { Id = 1, Enabled = true, InInventory = 800 };

        _mockUseCase.Setup(useCase => useCase.Apply(new Product { Name = "Anything" })).ReturnsAsync(product);

        var result = await controller.CreateProductAsync(productDto);


        Assert.IsType<OkResult>(result);
    }
}
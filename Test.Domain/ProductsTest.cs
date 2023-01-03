using Domain.UseCases.CreateProductUseCase;
using Domain.UseCases.CreateProductUseCase.Ports;
using Domain.UseCases.DeleteProductUseCase;
using Moq;
using Test.Common;

namespace Test.Domain;

public class ProductsTest
{
    private readonly Mock<IProductRepository> _productRepository;

    public ProductsTest()
    {
        var mockRepository = new MockRepository(MockBehavior.Default);
        _productRepository = mockRepository.Create<IProductRepository>();
    }

    [Fact]
    public async Task Create()
    {
        var product = Builder.GenerateData(1).FirstOrDefault();

        _productRepository.Setup(repo => repo.AddAsync(product)).ReturnsAsync(product);

        var useCase = new CreateProductUseCase(_productRepository.Object);

        var productCreated = await useCase.Apply(product);

        Assert.Equal(product.Name, productCreated.Name);
    }

    [Fact]
    public async Task Delete()
    {
        var product = Builder.GenerateData(1).FirstOrDefault();
        _productRepository.Setup(repo => repo.RemoveAsync(product)).ReturnsAsync(product);
        var useCase = new DeleteProductUseCase(_productRepository.Object);

        var productDeleted = await useCase.Apply(product);

        Assert.Equal(product, productDeleted);
    }
}
using Domain.UseCases.CreateProductUseCase;
using Domain.UseCases.CreateProductUseCase.Ports;
using Moq;
using Test.Domain.Helper;

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
        var product = Build.Product();

        _productRepository.Setup(repo => repo.AddAsync(product)).ReturnsAsync(product);

        var useCase = new CreateProductUseCase(_productRepository.Object);

        var productCreated = await useCase.Apply(product);

        Assert.Equal(Build.Product().Name, productCreated.Name);
    }
}
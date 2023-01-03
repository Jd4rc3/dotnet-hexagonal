using Domain.Models;
using Domain.UseCases.CreateProductUseCase;
using Domain.UseCases.CreateProductUseCase.Ports;
using Domain.UseCases.DeleteProductUseCase;
using Domain.UseCases.GetProductUseCase;
using Domain.UseCases.UpdateProductUseCase;
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
    public async Task CreateProductTest()
    {
        var product = Builder.GenerateProductsData(1).FirstOrDefault();

        _productRepository.Setup(repo => repo.AddAsync(product)).ReturnsAsync(product);

        var useCase = new CreateProductUseCase(_productRepository.Object);

        var productCreated = await useCase.Apply(product);

        Assert.Equal(product?.Name, productCreated.Name);
    }

    [Fact]
    public async Task DeleteProductTest()
    {
        var product = Builder.GenerateProductsData(1).FirstOrDefault();
        _productRepository.Setup(repo => repo.RemoveAsync(product)).ReturnsAsync(product);
        var useCase = new DeleteProductUseCase(_productRepository.Object);

        var productDeleted = await useCase.Apply(product);

        Assert.Equal(product, productDeleted);
    }

    [Fact]
    public async Task FindTestByIdTest()
    {
        var product = Builder.GenerateProductsData(1).First();
        _productRepository.Setup(repo => repo.FindByIdAsync(product)).ReturnsAsync(product);
        var useCase = new GetProductUseCase(_productRepository.Object);

        var foundProduct = await useCase.Apply(product);

        Assert.Equal(product, foundProduct);
    }

    [Fact]
    public async Task FindAllProductsTest()
    {
        _productRepository.Setup(p => p.FindAllAsync()).ReturnsAsync(Builder.GenerateProductsData(10));
        var useCase = new GetAllProductsUseCase(_productRepository.Object);

        var list = await useCase.Apply(It.IsAny<Product>());

        Assert.Equal(list.Count, 10);
    }

    [Fact]
    public async Task UpdateProductTest()
    {
        var productToUpdate = Builder.GenerateProductsData(1).First();
        _productRepository.Setup(r => r.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(productToUpdate);
        var useCase = new UpdateProductUseCase(_productRepository.Object);

        var updatedProduct = await useCase.Apply(productToUpdate);

        Assert.Equal(productToUpdate, updatedProduct);
    }
}
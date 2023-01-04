using Domain.Models;
using Domain.UseCases;
using Domain.UseCases.Ports;
using Moq;

namespace Test.Domain;

public class BuyUseCaseTest
{
    private readonly Mock<IBuyRepository> _mock;

    private readonly Mock<IProductRepository> _mockProduct;

    public BuyUseCaseTest()
    {
        var mockRepository = new MockRepository(MockBehavior.Default);

        _mock = mockRepository.Create<IBuyRepository>();

        _mockProduct = mockRepository.Create<IProductRepository>();
    }

    [Fact]
    public async Task Purchase()
    {
        var buy = new Buy
        {
            Id = 1, IdNumber = 123, IdType = "cc", ClientName = "nestor",
            Buys = new List<ProductBuy> { new() { Id = 1, Quantity = 10 } }
        };
        var product = new Product
            { Id = 1, Name = "product", Buys = new List<ProductBuy> { new() { Id = 1, Quantity = 10 } } };

        _mock.Setup(c => c.PurchaseAsync(It.IsAny<Buy>())).ReturnsAsync(buy);
        _mockProduct.Setup(p => p.FindByIdAsync(It.IsAny<Product>()))
            .ReturnsAsync(product);

        _mockProduct.Setup(p => p.UpdateAsync(It.IsAny<Product>()))
            .ReturnsAsync(product);

        var buyUseCase = new PurchaseUseCase(_mock.Object, _mockProduct.Object);

        var result = await buyUseCase.Apply(buy);

        Assert.NotNull(result);
    }
}
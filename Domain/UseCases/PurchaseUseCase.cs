using Domain.Models;
using Domain.UseCases.Ports;

namespace Domain.UseCases;

public class PurchaseUseCase : UseCase<Task<Buy>, Buy>
{
    private readonly IProductRepository _productRepository;
    private readonly IBuyRepository _repository;

    public PurchaseUseCase(IBuyRepository repository, IProductRepository productRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
    }

    public override async Task<Buy> Apply(Buy buy)
    {
        var productsList = new List<Product>();
        foreach (var productBuy in buy.Buys)
        {
            var foundProduct = await _productRepository.FindByIdAsync(productBuy.Product);

            ProductValidations(foundProduct, productBuy);

            foundProduct.InInventory -= productBuy.Quantity;
            productBuy.Product = foundProduct;

            productsList.Add(foundProduct);
        }

        var purchase = await _repository.PurchaseAsync(buy);
        await _productRepository.UpdateAsync(productsList);

        return purchase;
    }

    private static void ProductValidations(Product product, ProductBuy productBuy)
    {
        if (!product.Enabled) throw new Exception("Product not enabled");

        if (product.InInventory < productBuy.Quantity)
            throw new Exception("Product not in inventory");

        if (product.Max < productBuy.Quantity)
            throw new Exception("Product max quantity exceeded");

        if (product.Min > productBuy.Quantity)
            throw new Exception("Product min quantity exceeded");
    }
}

public interface IBuyRepository
{
    Task<Buy> PurchaseAsync(Buy buy);
}
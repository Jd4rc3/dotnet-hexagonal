using Domain.Models;
using Domain.UseCases.Ports;

namespace Domain.UseCases;

public class GetProductUseCase : UseCase<Task<Product>, Product>
{
    private readonly IProductRepository _productRepository;

    public GetProductUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public override async Task<Product> Apply(Product product)
    {
        var foundedProduct = await _productRepository.FindByIdAsync(product);

        if (foundedProduct is null)
            throw new Exception("Product not found");

        return foundedProduct;
    }
}
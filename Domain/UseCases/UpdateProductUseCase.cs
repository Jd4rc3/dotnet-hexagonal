using Domain.Models;
using Domain.UseCases.Ports;

namespace Domain.UseCases;

public class UpdateProductUseCase : UseCase<Task<Product>, Product>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public override async Task<Product> Apply(Product product)
    {
        return await _productRepository.UpdateAsync(product);
    }
}
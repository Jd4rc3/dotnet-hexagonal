using Domain.Models;
using Domain.UseCases.CreateProductUseCase.Ports;

namespace Domain.UseCases.DeleteProductUseCase;

public class DeleteProductUseCase : UseCase<Task<Product>, Product>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public override async Task<Product> Apply(Product product)
    {
        return await _productRepository.RemoveAsync(product);
    }
}
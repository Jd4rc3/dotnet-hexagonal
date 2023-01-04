using Domain.Models;
using Domain.UseCases.Ports;

namespace Domain.UseCases.CreateProductUseCase;

public class CreateProductUseCase : UseCase<Task<Product>, Product>
{
    private readonly IProductRepository _productRepository;

    public CreateProductUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public override async Task<Product> Apply(Product product)
    {
        return await _productRepository.AddAsync(product);
    }
}
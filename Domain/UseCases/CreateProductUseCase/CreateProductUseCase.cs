using Domain.Models;
using Domain.UseCases.CreateProductUseCase.Ports;

namespace Domain.UseCases.CreateProductUseCase;

public class CreateProductUseCase
{
    private readonly IProductRepository _productRepository;

    public CreateProductUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Create(Product product)
    {
        return await _productRepository.AddAsync(product);
    }
}
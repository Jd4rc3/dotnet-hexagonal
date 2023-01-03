using Domain.Models;
using Domain.UseCases.CreateProductUseCase.Ports;

namespace Domain.UseCases.GetProductUseCase;

public class GetAllProductsUseCase : UseCase<Task<List<Product>>, Product>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public override async Task<List<Product>> Apply(Product _)
    {
        return await _productRepository.FindAllAsync();
    }
}
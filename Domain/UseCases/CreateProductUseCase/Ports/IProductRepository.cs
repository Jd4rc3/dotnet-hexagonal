using Domain.Models;

namespace Domain.UseCases.CreateProductUseCase.Ports;

public interface IProductRepository
{
    Task<Product> AddAsync(Product product);
}
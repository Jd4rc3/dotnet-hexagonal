using Domain.Models;

namespace Domain.UseCases.Ports;

public interface IProductRepository
{
    Task<Product> AddAsync(Product product);

    Task<Product> RemoveAsync(Product product);

    Task<Product> FindByIdAsync(Product product);

    Task<List<Product>> FindAllAsync();

    Task<Product> UpdateAsync(Product product);

    Task<IEnumerable<Product>> UpdateAsync(IEnumerable<Product> products);
}
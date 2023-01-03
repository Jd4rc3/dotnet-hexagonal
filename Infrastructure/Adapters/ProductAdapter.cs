using Domain.Models;
using Domain.UseCases.CreateProductUseCase.Ports;

namespace Infrastructure.Adapters;

public class ProductAdapter : IProductRepository
{
    private readonly Context _context;

    public ProductAdapter(Context context)
    {
        _context = context;
    }

    public async Task<Product> AddAsync(Product product)
    {
        var newProduct = await _context.Product.AddAsync(product);

        return newProduct.Entity;
    }
}
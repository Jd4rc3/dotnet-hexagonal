using Domain.Models;
using Domain.UseCases.CreateProductUseCase.Ports;
using Microsoft.EntityFrameworkCore;

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

        await _context.SaveChangesAsync();

        return newProduct.Entity;
    }

    public async Task<Product> RemoveAsync(Product product)
    {
        var deletedProduct = _context.Product.Remove(product);

        await _context.SaveChangesAsync();

        return deletedProduct.Entity;
    }

    public async Task<Product> FindByIdAsync(Product product)
    {
        return await _context.Product.FirstOrDefaultAsync(p => p.Id == product.Id);
    }

    public async Task<List<Product>> FindAllAsync()
    {
        return await _context.Product.ToListAsync();
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        var oldProduct = _context.Product.FirstOrDefault(p => p.Id == product.Id);
        if (oldProduct is null)
            throw new Exception("Product not found");

        _context.Product.Update(product);
        await _context.SaveChangesAsync();

        return product;
    }
}
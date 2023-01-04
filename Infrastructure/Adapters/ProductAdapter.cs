using Domain.Models;
using Domain.UseCases.Ports;
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
        return await _context.FindAsync<Product>(product.Id);
    }

    public async Task<List<Product>> FindAllAsync()
    {
        return await _context.Product.ToListAsync();
    }


    public async Task<Product> UpdateAsync(Product product)
    {
        var productToUpdate = await _context.FindAsync<Product>(product.Id);

        if (productToUpdate is null)
            throw new Exception("Product not found");

        productToUpdate.Name = product.Name;
        productToUpdate.Enabled = product.Enabled;
        productToUpdate.Max = product.Max;
        productToUpdate.Min = product.Min;
        productToUpdate.InInventory = product.InInventory;

        await _context.SaveChangesAsync();

        return productToUpdate;
    }

    public async Task<IEnumerable<Product>> UpdateAsync(IEnumerable<Product> products)
    {
        _context.UpdateRange(products);

        await _context.SaveChangesAsync();

        return products;
    }
}
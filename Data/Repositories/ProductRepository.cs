namespace DotNet.Redis.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNet.Redis.Entities;
using Microsoft.EntityFrameworkCore;
using DotNet.Redis.Data;

public class ProductRepository : IProductRepository
{
    private readonly DataContext _context;

    public ProductRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<ProductEntity> GetProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return product;
    }

    public async Task<IEnumerable<ProductEntity>> GetProductsAsync()
    {
        var products = await _context.Products.ToListAsync();
        return products;
    }

    public async Task<ProductEntity> CreateProductAsync(ProductEntity product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<ProductEntity> UpdateProductAsync(ProductEntity product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
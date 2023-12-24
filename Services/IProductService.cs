namespace DotNet.Redis.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNet.Redis.Models;


public interface IProductService
{
    Task<Product> GetProductAsync(int id);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> CreateProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
}
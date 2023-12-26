namespace DotNet.Redis.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNet.Redis.Entities;


public interface IProductRepository
{
    Task<ProductEntity> GetProductAsync(int id);
    Task<IEnumerable<ProductEntity>> GetProductsAsync();
    Task<ProductEntity> CreateProductAsync(ProductEntity product);
    Task<ProductEntity> UpdateProductAsync(ProductEntity product);
    Task DeleteProductAsync(int id);
}
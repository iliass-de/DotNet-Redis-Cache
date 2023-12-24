namespace DotNet.Redis.Mappers;
using DotNet.Redis.Entities;
using DotNet.Redis.Models;

public interface IProductMapper
{
    ProductEntity ToEntity(Product product);
    Product ToModel(ProductEntity product);
    IEnumerable<Product> ToModels(IEnumerable<ProductEntity> products);
    IEnumerable<ProductEntity> ToEntities(IEnumerable<Product> products);
}
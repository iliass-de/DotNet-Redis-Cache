namespace DotNet.Redis.Mappers;
using DotNet.Redis.Entities;
using DotNet.Redis.Models;

public class ProductMapper : IProductMapper
{
    public ProductEntity ToEntity(Product product)
    {
        return new ProductEntity
        {
            ID = product.ID,
            Name = product.Name,
            Description = product.Description,
            Category = product.Category
        };
    }

    public Product ToModel(ProductEntity product)
    {
        return new Product
        {
            ID = product.ID,
            Name = product.Name,
            Description = product.Description,
            Category = product.Category
        };
    }

    public IEnumerable<Product> ToModels(IEnumerable<ProductEntity> products)
    {
        return products.Select(ToModel);
    }

    public IEnumerable<ProductEntity> ToEntities(IEnumerable<Product> products)
    {
        return products.Select(ToEntity);
    }
}
namespace DotNet.Redis.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNet.Redis.Models;
using DotNet.Redis.Entities;
using DotNet.Redis.Repositories;
using DotNet.Redis.Mappers;


public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductMapper _productMapper;

    public ProductService(IProductRepository productRepository, IProductMapper productMapper)
    {
        _productRepository = productRepository;
        _productMapper = productMapper;
    }

    public async Task<Product> GetProductAsync(int id)
    {
        var product = await _productRepository.GetProductAsync(id);
        return _productMapper.ToModel(product);
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var products = await _productRepository.GetProductsAsync();
        return _productMapper.ToModels(products);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        var productEntity = _productMapper.ToEntity(product);
        var createdProduct = await _productRepository.CreateProductAsync(productEntity);
        return _productMapper.ToModel(createdProduct);
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        var productEntity = _productMapper.ToEntity(product);
        var updatedProduct = await _productRepository.UpdateProductAsync(productEntity);
        return _productMapper.ToModel(updatedProduct);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteProductAsync(id);
    }
}
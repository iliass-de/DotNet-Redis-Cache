namespace DotNet.Redis.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNet.Redis.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using DotNet.Redis.Services;

[ApiController]
public class ProductController : ControllerBase
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;
    public ProductController(ILogger<ProductController> logger, IProductService productService, IDistributedCache cache)
    {
        _logger = logger;
        _productService = productService;
        _cache = cache;
    }

    [HttpGet]
    [Route("products/{cacheenbaled}")]
    public async Task<IActionResult> GetProducts(bool cacheenbaled)
    {
        if (cacheenbaled)
        {
            var products = await _cache.GetStringAsync("products");
            if (products != null)
            {
                _logger.LogInformation("Retrieved products from cache");
                 return Ok(JsonSerializer.Deserialize<List<Product>>(products));
            }
        }
        var productsFromDb = await _productService.GetProductsAsync();
        if (cacheenbaled)
        {
            _logger.LogInformation("Setting products in cache");
            await _cache.SetStringAsync($"products", JsonSerializer.Serialize(productsFromDb));
        }
        return Ok(productsFromDb);
    }

    [HttpGet]
    [Route("products/{id}/{cacheenbaled}")]
    public async Task<IActionResult> GetProduct(int id, bool cacheenbaled)
    {
        if (cacheenbaled)
        {
            _logger.LogInformation("Retrieving product from cache");
            var product = await _cache.GetStringAsync($"product-{id}");
            if (product != null)
            {
                return Ok(JsonSerializer.Deserialize<Product>(product));
            }
        }
        var productFromDb = await _productService.GetProductAsync(id);
        if (cacheenbaled)
        {
            _logger.LogInformation("Setting product in cache");
            await _cache.SetStringAsync($"product-{id}", JsonSerializer.Serialize(productFromDb));
        }
        return Ok(productFromDb);
    }

    [HttpPost]
    [Route("product/{cacheenbaled}")]
    public async Task<IActionResult> AddProduct(Product product, bool cacheenbaled)
    {
        await _productService.CreateProductAsync(product);
        if (cacheenbaled)
        {
            _logger.LogInformation("Setting products in cache");
            var productFromDb = await _productService.GetProductAsync(product.ID);
            await _cache.SetStringAsync($"product-{product.ID}", JsonSerializer.Serialize(productFromDb));
            var products = await _productService.GetProductsAsync();
            await _cache.SetStringAsync($"products", JsonSerializer.Serialize(products));
        }
        return Ok(product);
    }
    

    [HttpPut]
    [Route("product/{cacheenbaled}")]
    public async Task<IActionResult> UpdateProduct(Product product, bool cacheenbaled)
    {
        await _productService.UpdateProductAsync(product);
        if (cacheenbaled)
        {
            _logger.LogInformation("Setting products in cache");
            var productFromDb = await _productService.GetProductAsync(product.ID);
            await _cache.SetStringAsync($"product-{product.ID}", JsonSerializer.Serialize(productFromDb));
            _logger.LogInformation("Updating products in cache ");
            var products = await _productService.GetProductsAsync();
            await _cache.SetStringAsync($"products", JsonSerializer.Serialize(products));
        
        }
        return Ok(product);
    }

    [HttpDelete]
    [Route("product/{id}/{cacheenbaled}")]
    public async Task<IActionResult> DeleteProduct(int id, bool cacheenbaled)
    {
        await _productService.DeleteProductAsync(id);
        if (cacheenbaled)
        {
            _logger.LogInformation("Removing product from cache");
            await _cache.RemoveAsync($"product-{id}");
            _logger.LogInformation("Updating products in cache ");
            var products = await _productService.GetProductsAsync();
            await _cache.SetStringAsync($"products", JsonSerializer.Serialize(products));
        
        }
        return Ok();
    }

}

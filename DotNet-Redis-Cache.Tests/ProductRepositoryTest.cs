using System.Threading.Tasks;
namespace DotNet_Redis_Cache.Tests
{
  

    public class ProductRepositoryTest
    {
        private readonly Mock<DataContext> _dbContextMock;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryTest()
        {
            _dbContextMock = new Mock<DataContext>();
            _productRepository = new ProductRepository(_dbContextMock.Object);
        }
        [Fact]
        public async Task GetProductAsync_ShouldReturnProduct()
        {
            // Arrange
            var product = new ProductEntity
            {
                ID = 1,
                Name = "Product 1",
                Description = "Product 1 Description"
            };

            _dbContextMock.Setup(x => x.Products.Add(product));
            // Act
            var result = await _productRepository.GetProductAsync(1);
            // Assert
            Assert.Equal(product, result);
        }
        [Fact]
        public async Task GetProductsAsync_ShouldReturnProducts()
        {
            // Arrange
            var products = new List<ProductEntity>
            {
                new ProductEntity
                {
                    ID = 1,
                    Name = "Product 1",
                    Description = "Product 1 Description"
                },
                new ProductEntity
                {
                    ID = 2,
                    Name = "Product 2",
                    Description = "Product 2 Description"
                }
            };
            _dbContextMock.Setup(x => x.Products.Add(products[0]));
            _dbContextMock.Setup(x => x.Products.Add(products[1]));
            // Act
            var result = await _productRepository.GetProductsAsync();
            // Assert
            Assert.Equal(products, result);
        }
        [Fact]
        public async Task CreateProductAsync_ShouldReturnCreatedProduct()
        {
            // Arrange
            var product = new ProductEntity
            {
                ID = 1,
                Name = "Product 1",
                Description = "Product 1 Description"
            };
            _dbContextMock.Setup(x => x.Products.Add(product));
            // Act
            var result = await _productRepository.CreateProductAsync(product);
            // Assert
            Assert.Equal(product, result);
        }
        [Fact]
        public async Task UpdateProductAsync_ShouldReturnUpdatedProduct()
        {
            // Arrange
            var product = new ProductEntity
            {
                ID = 1,
                Name = "Product 1",
                Description = "Product 1 Description"
            };
            _dbContextMock.Setup(x => x.Products.Update(product));
            // Act
            var result = await _productRepository.UpdateProductAsync(product);
            // Assert
            Assert.Equal(product, result);
        }
        [Fact]
        public async Task DeleteProductAsync_ShouldReturnDeletedProduct()
        {
            // Arrange
            var product = new ProductEntity
            {
                ID = 1,
                Name = "Product 1",
                Description = "Product 1 Description"
            };
            _dbContextMock.Setup(x => x.Products.Remove(product));
            // Act
            await _productRepository.DeleteProductAsync(1);
            // Assert
            _dbContextMock.Verify(x => x.Products.Remove(product), Times.Once);
        }

    
    }
}
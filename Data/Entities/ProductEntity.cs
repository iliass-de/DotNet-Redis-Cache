using DotNet.Redis.Enums;
namespace DotNet.Redis.Entities
{
    public record ProductEntity
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public ProductCategory Category { get; init; }
    }
}
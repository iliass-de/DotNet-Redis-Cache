namespace DotNet.Redis.Models;
using DotNet.Redis.Enums;
public record Product
{
    public int ID { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public ProductCategory Category { get; init; }
}
namespace DotNet.Redis.Data;
using Microsoft.EntityFrameworkCore;
using DotNet.Redis.Entities;

public class DataContext: DbContext 
{
    protected readonly IConfiguration _configuration;
    public DataContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings

        var serviceConnection = _configuration.GetSection("ServiceConnection");
        var connection = serviceConnection.GetValue<string>("Postgres");
        options.UseNpgsql(connection);
    }

    public DbSet<ProductEntity> Products { get; init; }   
}
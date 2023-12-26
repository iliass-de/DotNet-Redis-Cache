using DotNet.Redis.Data;
using DotNet.Redis.Mappers;
using DotNet.Redis.Services;
using DotNet.Redis.Repositories;


var builder = WebApplication.CreateBuilder(args);
var serviceConnection = builder.Configuration.GetSection("ServiceConnection");
var redisConnection = serviceConnection.GetValue<string>("Redis");

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DataContext>();
builder.Services.AddControllers();
builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = redisConnection; }); 
builder.Services.AddScoped<IProductMapper, ProductMapper>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
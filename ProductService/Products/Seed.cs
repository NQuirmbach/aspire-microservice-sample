using System.Text.Json;
using StackExchange.Redis;

namespace ProductService.Products;

public static class Seed
{
    public static async Task SeedProductsAsync(this WebApplication app)
    {
        var factory = app.Services.GetRequiredService<IHttpClientFactory>();
        var database = app.Services.GetRequiredService<IDatabase>();
        var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("ProductSeed");
        
        logger.LogInformation("Seeding products...");
        
        using var client = factory.CreateClient("DummyJson");

        var result = await client.GetFromJsonAsync<GetProductsResponse>("/product");
        if (result is null)
            throw new Exception("Cannot load seed data for products");
        
        logger.LogInformation("Storing {Count} products", result.Products.Length);

        var json = JsonSerializer.Serialize(result.Products);
        await database.StringSetAsync("products", json);
    }

    private record GetProductsResponse(Product[] Products);
}
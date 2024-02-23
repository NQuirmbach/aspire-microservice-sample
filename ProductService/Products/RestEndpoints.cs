using System.Text.Json;
using StackExchange.Redis;

namespace ProductService.Products;

public static class RestEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGetProducts();
    }

    private static void MapGetProducts(this IEndpointRouteBuilder app) =>
        app.MapGet("/product", async (IDatabase database) =>
            {
                var json = await database.StringGetAsync("products");
                if (!json.HasValue)
                    return Results.Problem("Products have not been cached");
                
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json!);

                return Results.Ok(products);
            })
            .Produces<Product[]>()
            .WithName("GetProducts")
            .WithOpenApi();
}
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace ProductService.Config;

public static class RedisConfig
{
    public static void AddRedisServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IDatabase>(s =>
        {
            var logger = s.GetRequiredService<ILoggerFactory>().CreateLogger("RedisConfig");
            var connectionString = builder.Configuration.GetConnectionString("Redis")!;

            if (builder.Environment.IsDevelopment())
            {
                logger.LogInformation("Found redis connection string: {ConnectionString}", connectionString);
            }

            var connection = ConnectionMultiplexer.Connect(connectionString);

            return connection.GetDatabase();
        });
    }
}

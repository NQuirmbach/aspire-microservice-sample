var builder = DistributedApplication.CreateBuilder(args);

// 3rd party services
var postgres = builder.AddPostgresContainer("postgres");
var redis = builder.AddRedisContainer("redis");

// Microservices
builder.AddProject("product-service", "../../ProductService/ProductService/ProductService.csproj")
    .WithReference(redis, "Redis");
 
builder.Build().Run();
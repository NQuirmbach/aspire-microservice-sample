var builder = DistributedApplication.CreateBuilder(args);

// 3rd party services
var postgres = builder.AddPostgresContainer("postgres");
var redis = builder.AddRedisContainer("redis");

// Microservices

// Referencing a project out of the solution is not possible
// Possible (but not really feasible) solution without referencing the project itself
// https://github.com/dotnet/aspire/discussions/1137#discussioncomment-7713391

// builder.AddProject("product-service", "../../ProductService/ProductService/ProductService.csproj")
//     .WithReference(redis, "Redis");

// Project needs to be referenced in AppHost project
builder.AddProject<Projects.ProductService>("product-service")
    .WithReference(redis, "Redis");

// builder.AddExecutable()

builder.Build().Run();
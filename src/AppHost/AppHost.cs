var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.Inventory_Api>("inventory-api");

builder.AddProject<Projects.Web>("web")
    .WithReference(api)
    .WaitFor(api)
    .WithExternalHttpEndpoints();

builder.Build().Run();

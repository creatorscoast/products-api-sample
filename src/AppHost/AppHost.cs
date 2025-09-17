var builder = DistributedApplication.CreateBuilder(args);

var username = builder.AddParameter("Username");
var password = builder.AddParameter("Password", secret: true);
var server = builder.AddPostgres("postgres", username,password, 5432)
    .WithImage("postgres:17")                         
    .WithLifetime(ContainerLifetime.Persistent);

var db = server.AddDatabase("inventorydb");

var api = builder.AddProject<Projects.Inventory_Api>("api")
    .WithReference(db)
    .WaitFor(db);

builder.AddProject<Projects.Web>("web")
    .WithReference(api)
    .WaitFor(api)
    .WithExternalHttpEndpoints();

builder.Build().Run();
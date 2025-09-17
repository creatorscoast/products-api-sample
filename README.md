## products-api-sample

Sample Inventory .NET API using FastEndpoints, Dapper, PostgreSQL, and xUnit integration tests with Testcontainers. This is currently a WIP and contains duplication of mapping and some request/response models. The idea is to evolve and cleanup after the basic vertical slices are setup.

![App](https://github.com/creatorscoast/products-api-sample/blob/main/app.png)

### Project structure

```text
c:\_Dev\products-api-sample\
  - Main.sln
  - README.md
  - src\
    - Inventory.Api\
      - Program.cs
      - appsettings.json
      - Database\
        - IDbConnectionFactory.cs
        - DatabaseInitializer.cs
      - Entities\
      - Features\
        - Products\
          - CreateProduct\
          - GetProduct\
          - UpdateProduct\
          - DeleteProduct\
      - Services\
        - IProductService.cs
        - Impl\
          - ProductService.cs
    - IntegrationTests\
      - IntegrationTests.csproj
      - (tests using WebApplicationFactory + PostgreSQL Testcontainer)
```

### Requirements

- **.NET SDK**: 9.0+
- **Docker**: Required for running integration tests (Testcontainers spins up PostgreSQL)
- **PostgreSQL**: Only needed to run the API locally if not using containers; configure via connection string

### Configuration

- **Connection string**: `ConnectionStrings:DefaultConnectionString` in `src/Inventory.Api/appsettings.json`.
  - Default: `Host=localhost;Port=5432;Database=inventory;Username=postgres;Password=password`
  - Override via environment variable: `ConnectionStrings__DefaultConnectionString`.

Test Flow:

- Start a disposable PostgreSQL container
- Init Database
- Inject its connection string into the API in-memory server
- Warm up to initialize the database
- Validate create/get/list product endpoints

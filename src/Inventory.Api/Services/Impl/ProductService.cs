using Dapper;
using Inventory.Api.Database;
using Inventory.Api.Entities;

namespace Inventory.Api.Services.Impl;

public sealed class ProductService : IProductService
{
    private readonly IDbConnectionFactory _connectionFactory;

    public ProductService(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<int?> CreateAsync(Product product)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var id = await connection.ExecuteScalarAsync<int>(@"
            INSERT INTO products (name, description, price, createdAt, updatedAt)
            VALUES (@Name, @Description, @Price, NOW(), NOW()) 
            RETURNING id;",
            product);

        return id;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        var result = await connection.ExecuteAsync(
            @"DELETE FROM products WHERE id = @Id",
            new { Id = id });

        return result > 0;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QueryAsync<Product>("SELECT * FROM products ORDER BY id");
    }

    public async Task<Product?> GetAsync(int id)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QuerySingleOrDefaultAsync<Product>(
            "SELECT * FROM products WHERE id = @Id LIMIT 1",
            new { Id = id });
    }

    public async Task<IEnumerable<Product>> SearchByNameAsync(string searchTerm)
    {
        using var connection = await _connectionFactory.CreateConnectionAsync();

        return await connection.QueryAsync<Product>(
            "SELECT * FROM products WHERE name ILIKE '%' || @SearchTerm || '%'",
            new { SearchTerm = searchTerm });
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        var existingProduct = await GetAsync(product.Id);
        if (existingProduct is null) return false;

        using var connection = await _connectionFactory.CreateConnectionAsync();
        var result = await connection.ExecuteAsync(@"
            UPDATE products
            SET description = @Description,
                price = @Price,
                updatedAt = NOW()
            WHERE id = @Id;",
            product);

        return result > 0;
    }
}

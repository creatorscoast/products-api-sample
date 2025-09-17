using Inventory.Api.Entities;

namespace Inventory.Api.Services;

public interface IProductService
{
    Task<int?> CreateAsync(Product product);

    Task<Product?> GetAsync(int id);

    Task<IEnumerable<Product>> GetAllAsync();

    Task<bool> UpdateAsync(Product product);

    Task<bool> DeleteAsync(int id);
}



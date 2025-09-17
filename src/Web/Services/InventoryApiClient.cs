using System.Net.Http.Json;

namespace Web.Services
{
    public interface IInventoryApiClient
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken ct = default);
        Task<ProductDto?> GetProductAsync(int id, CancellationToken ct = default);
        Task<int> CreateProductAsync(CreateProductDto request, CancellationToken ct = default);
        Task UpdateProductAsync(UpdateProductDto request, CancellationToken ct = default);
        Task DeleteProductAsync(int id, CancellationToken ct = default);
    }

    public sealed class InventoryApiClient : IInventoryApiClient
    {
        private readonly HttpClient _httpClient;

        public InventoryApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken ct = default)
            => await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("/products", ct)
                ?? Enumerable.Empty<ProductDto>();

        public async Task<ProductDto?> GetProductAsync(int id, CancellationToken ct = default)
            => await _httpClient.GetFromJsonAsync<ProductDto>($"/products/{id}", ct);

        public async Task<int> CreateProductAsync(CreateProductDto request, CancellationToken ct = default)
        {
            var resp = await _httpClient.PostAsJsonAsync("/products", request, ct);
            resp.EnsureSuccessStatusCode();
            var created = await resp.Content.ReadFromJsonAsync<ProductDto>(cancellationToken: ct);
            return created?.Id ?? 0;
        }

        public async Task UpdateProductAsync(UpdateProductDto request, CancellationToken ct = default)
        {
            var resp = await _httpClient.PutAsJsonAsync($"/products/{request.Id}", request, ct);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync(int id, CancellationToken ct = default)
        {
            var resp = await _httpClient.DeleteAsync($"/products/{id}", ct);
            resp.EnsureSuccessStatusCode();
        }
    }
}



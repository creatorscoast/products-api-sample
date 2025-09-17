using Web.Components;
using Web.Services;

namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        // Configure API HttpClient
        var apiBaseUrl = builder.Configuration.GetSection("InventoryApi")["BaseUrl"];
        if (string.IsNullOrWhiteSpace(apiBaseUrl))
        {
            apiBaseUrl = Environment.GetEnvironmentVariable("INVENTORY_API_BASEURL") ?? "https://localhost:7122";
        }

        builder.Services.AddHttpClient<IInventoryApiClient, InventoryApiClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl!);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}

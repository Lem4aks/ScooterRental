using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using static RentalFront.Layout.MainLayout;

namespace RentalFront
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7029/") });
            builder.Services.AddBlazoredLocalStorage();
            await builder.Build().RunAsync();
        }
    }
}

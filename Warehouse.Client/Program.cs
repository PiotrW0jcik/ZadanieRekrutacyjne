using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Warehouse.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var url = builder.Configuration.GetValue<string>("Api");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url) });

builder.Services.AddMudServices();

await builder.Build().RunAsync();

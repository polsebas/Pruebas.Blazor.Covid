using Pruebas.Blazor.Covid;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using BlazorStrap;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// Configuración de MemoryCache
builder.Services.AddMemoryCache();
builder.Services.AddScoped<ApiService>();
builder.Services.AddBlazorStrap();

//builder.Configuration.AddJsonFile("appsettings.json");
//builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


await builder.Build().RunAsync();
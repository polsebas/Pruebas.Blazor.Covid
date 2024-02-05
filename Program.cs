using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

namespace Pruebas.Blazor.Covid
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            // Configuración del HttpClient utilizando HttpClientFactory
            //builder.Services.AddScoped(sp =>
            //{
            //    var httpClient = new HttpClient
            //    {
            //        //BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            //        BaseAddress = new Uri("http://localhost:5028")
            //    };

            //    // Puedes agregar configuraciones adicionales aquí si es necesario

            //    return httpClient;
            //});


            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient<Services.ApiService>(client =>
            {
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler
                {
                    // Configuraciones adicionales si es necesario
                };
            });

            builder.Services.AddScoped<Services.ApiService>();

            await builder.Build().RunAsync();
        }
    }
}

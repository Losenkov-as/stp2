using BlazorApp.Helpers;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<IAlertService, AlertService>()
                .AddScoped<IHttpService, HttpService>()
                .AddScoped<ILocalStorageService, LocalStorageService>();

            // configure http client
            builder.Services.AddScoped(x => {
                var apiUrl = new Uri(builder.Configuration["apiUrl"]);

                // use fake backend if "fakeBackend" is "true" in appsettings.json
                if (builder.Configuration["fakeBackend"] == "true")
                {
                    var fakeBackendHandler = new FakeBackendHandler(x.GetService<ILocalStorageService>());
                    return new HttpClient(fakeBackendHandler) { BaseAddress = apiUrl };
                }

                return new HttpClient() { BaseAddress = apiUrl };
            });

            var host = builder.Build();

            var accountService = host.Services.GetRequiredService<IAccountService>();
            await accountService.Initialize();

            var roleService = host.Services.GetRequiredService<IRoleService>();
            await roleService.Initialize();

            await host.RunAsync();
        }
    }
}
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
                .AddScoped<IBuildService, BuildService>()
                .AddScoped<IRoomService, RoomService>()
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<ILocationService, LocationService>()
                .AddScoped<IAlertService, AlertService>()
                .AddScoped<IHttpService, HttpService>()
                .AddScoped<IStatusService, StatusService>()
                .AddScoped<IMachineService, MachineService>()
                .AddScoped<IMachineTypeService, MachineTypeService>()
                .AddScoped<IMachineModelService, MachineModelService>()
                .AddScoped<IMaintenanceService, MaintenanceService>()
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

            var BuildService = host.Services.GetRequiredService<IBuildService>();
            await BuildService.Initialize();

            var locationService = host.Services.GetRequiredService<ILocationService>();
            await locationService.Initialize();

            var roomService = host.Services.GetRequiredService<IRoomService>();
            await roomService.Initialize();

            var machineService = host.Services.GetRequiredService<IMachineService>();
            await machineService.Initialize();

            var machinetypeService = host.Services.GetRequiredService<IMachineTypeService>();
            await machinetypeService.Initialize();
           
            var machinemodelService = host.Services.GetRequiredService<IMachineModelService>();
            await machinemodelService.Initialize();

            var statusService = host.Services.GetRequiredService<IStatusService>();
            await statusService.Initialize();

            var maintenanceService = host.Services.GetRequiredService<IMaintenanceService>();
            await maintenanceService.Initialize();

            await host.RunAsync();
        }
    }
}
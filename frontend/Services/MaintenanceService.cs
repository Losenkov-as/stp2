using BlazorApp.Models;
using BlazorApp.Models.Account;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BlazorApp.Services
{
    public interface IMaintenanceService
    {
        Maintenance Maintenance { get; }
        Task Initialize();
        Task Create(AddMaintenance model);
        Task<IList<Maintenance>> GetAll();
        Task<Maintenance> GetById(string id);
        Task Update(int id, EditMaintenance model);
        Task Delete(string id);
    }

    public class MaintenanceService : IMaintenanceService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _maintenanceKey = "maintenance";

        public Maintenance Maintenance { get; private set; }

        public MaintenanceService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        ) {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }

        public async Task Initialize()
        {
            Maintenance = await _localStorageService.GetItem<Maintenance>(_maintenanceKey);
        }


        public async Task Create(AddMaintenance model)
        {
            await _httpService.Post("/maintenances/create", model);
        }

        public async Task<IList<Maintenance>> GetAll()
        {
            return await _httpService.Get<IList<Maintenance>>("/maintenances");
        }

        public async Task<Maintenance> GetById(string id)
        {
            return await _httpService.Get<Maintenance>($"/maintenances/{id}");
        }

        public async Task Update(int id, EditMaintenance model)
        {
            await _httpService.Put($"/maintenances/{id}", model);

            // update stored Maintenance if the logged in Maintenance updated their own record
            if (id == int.Parse(Maintenance.Id)) 
            {
                // update local storage
                //Maintenance.Name = model.Name;
                await _localStorageService.SetItem(_maintenanceKey, Maintenance);
            }
        }

        public async Task Delete(string id)
        {
            await _httpService.Delete($"/maintenances/{id}");
        }

    }
}
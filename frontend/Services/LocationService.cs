using BlazorApp.Models;
using BlazorApp.Models.Account;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface ILocationService
    {
        Location Location { get; }
        Task Initialize();
        Task Create(AddLocation model);
        Task<IList<Location>> GetAll();
        Task<Location> GetById(string id);
        Task Update(string id, EditLocation model);
        Task Delete(string id);
    }

    public class LocationService : ILocationService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _locationKey = "location";

        public Location Location { get; private set; }

        public LocationService(
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
            Location = await _localStorageService.GetItem<Location>(_locationKey);
        }


        public async Task Create(AddLocation model)
        {
            await _httpService.Post("/locations/create", model);
        }

        public async Task<IList<Location>> GetAll()
        {
            return await _httpService.Get<IList<Location>>("/locations");
        }

        public async Task<Location> GetById(string id)
        {
            return await _httpService.Get<Location>($"/locations/{id}");
        }

        public async Task Update(string id, EditLocation model)
        {
            await _httpService.Put($"/locations/{id}", model);

            // update stored Location if the logged in Location updated their own record
            if (id == Location.Id.ToString()) 
            {
                // update local storage
                Location.Workshop = model.Workshop;
                Location.Plot = model.Plot;
                await _localStorageService.SetItem(_locationKey, Location);
            }
        }

        public async Task Delete(string id)
        {
            await _httpService.Delete($"/locations/{id}");
        }
    }
}
using BlazorApp.Models;
using BlazorApp.Models.Account;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IRoomService
    {
        Room Room { get; }
        Task Initialize();
        Task Create(AddRoom model);
        Task<IList<Room>> GetAll();
        Task<Room> GetById(string id);
        Task Update(string id, EditRoom model);
        Task Delete(string id);
    }

    public class RoomService : IRoomService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _RoomKey = "room";

        public Room Room { get; private set; }

        public RoomService(
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
            Room = await _localStorageService.GetItem<Room>(_RoomKey);
        }


        public async Task Create(AddRoom model)
        {
            await _httpService.Post("/rooms/create", model);
        }

        public async Task<IList<Room>> GetAll()
        {
            return await _httpService.Get<IList<Room>>("/rooms");
        }

        public async Task<Room> GetById(string id)
        {
            return await _httpService.Get<Room>($"/rooms/{id}");
        }

        public async Task Update(string id, EditRoom model)
        {
            await _httpService.Put($"/rooms/{id}", model);

            // update stored Room if the logged in Room updated their own record
            if (id == Room.Id.ToString()) 
            {
                // update local storage
                Room.Name = model.Name;
                await _localStorageService.SetItem(_RoomKey, Room);
            }
        }

        public async Task Delete(string id)
        {
            await _httpService.Delete($"/rooms/{id}");
        }
    }
}
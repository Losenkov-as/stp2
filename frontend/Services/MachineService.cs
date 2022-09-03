using BlazorApp.Models;
using BlazorApp.Models.Account;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IMachineService
    {
        Machine Machine { get; }
        Task Initialize();
        Task Create(AddMachine model);
        Task<IList<Machine>> GetAll();
        Task<Machine> GetById(string id);
        Task Update(string id, EditMachine model);
        Task Delete(string id);
    }

    public class MachineService : IMachineService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _machineKey = "machine";

        public Machine Machine { get; private set; }

        public MachineService(
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
            Machine = await _localStorageService.GetItem<Machine>(_machineKey);
        }


        public async Task Create(AddMachine model)
        {
            await _httpService.Post("/machines/create", model);
        }

        public async Task<IList<Machine>> GetAll()
        {
            return await _httpService.Get<IList<Machine>>("/machines");
        }

        public async Task<Machine> GetById(string id)
        {
            return await _httpService.Get<Machine>($"/machines/{id}");
        }

        public async Task Update(string id, EditMachine model)
        {
            await _httpService.Put($"/machines/{id}", model);

            // update stored Machine if the logged in Machine updated their own record
            if (id == Machine.Id) 
            {
                // update local storage
                Machine.InventoryNumber = model.InventoryNumber;
                await _localStorageService.SetItem(_machineKey, Machine);
            }
        }

        public async Task Delete(string id)
        {
            await _httpService.Delete($"/machines/{id}");
        }
    }
}
using BlazorApp.Models;
using BlazorApp.Models.Account;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IMachineTypeService
    {
        MachineType MachineType { get; }
        Task Initialize();
        Task Create(AddMachineType model);
        Task<IList<MachineType>> GetAll();
        Task<MachineType> GetById(string id);
        Task Update(string id, EditMachineType model);
        Task Delete(string id);
    }

    public class MachineTypeService : IMachineTypeService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _MachineTypeKey = "MachineType";

        public MachineType MachineType { get; private set; }

        public MachineTypeService(
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
            MachineType = await _localStorageService.GetItem<MachineType>(_MachineTypeKey);
        }


        public async Task Create(AddMachineType model)
        {
            await _httpService.Post("/MachineTypes/create", model);
        }

        public async Task<IList<MachineType>> GetAll()
        {
            return await _httpService.Get<IList<MachineType>>("/MachineTypes");
        }

        public async Task<MachineType> GetById(string id)
        {
            return await _httpService.Get<MachineType>($"/MachineTypes/{id}");
        }

        public async Task Update(string id, EditMachineType model)
        {
            await _httpService.Put($"/MachineTypes/{id}", model);

            // update stored MachineType if the logged in MachineType updated their own record
            if (id == MachineType.Id.ToString()) 
            {
                // update local storage
                MachineType.Name = model.Name;
                await _localStorageService.SetItem(_MachineTypeKey, MachineType);
            }
        }

        public async Task Delete(string id)
        {
            await _httpService.Delete($"/MachineTypes/{id}");
        }
    }
}
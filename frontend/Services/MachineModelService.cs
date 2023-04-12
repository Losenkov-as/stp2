using BlazorApp.Models;
using BlazorApp.Models.Account;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IMachineModelService
    {
        MachineModel MachineModel { get; }
        Task Initialize();
        Task Create(AddMachineModel model);
        Task<IList<MachineModel>> GetAll();
        Task<MachineModel> GetById(string id);
        Task Update(string id, EditMachineModel model);
        Task Delete(string id);
    }

    public class MachineModelService : IMachineModelService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _MachineModelKey = "MachineModel";

        public MachineModel MachineModel { get; private set; }

        public MachineModelService(
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
            MachineModel = await _localStorageService.GetItem<MachineModel>(_MachineModelKey);
        }


        public async Task Create(AddMachineModel model)
        {
            await _httpService.Post("/MachineModels/create", model);
        }

        public async Task<IList<MachineModel>> GetAll()
        {
            return await _httpService.Get<IList<MachineModel>>("/MachineModels");
        }

        public async Task<MachineModel> GetById(string id)
        {
            return await _httpService.Get<MachineModel>($"/MachineModels/{id}");
        }

        public async Task Update(string id, EditMachineModel model)
        {
            await _httpService.Put($"/MachineModels/{id}", model);

            // update stored MachineModel if the logged in MachineModel updated their own record
            if (id == MachineModel.Id.ToString()) 
            {
                // update local storage
                MachineModel.Name = model.Name;
                await _localStorageService.SetItem(_MachineModelKey, MachineModel);
            }
        }

        public async Task Delete(string id)
        {
            await _httpService.Delete($"/MachineModels/{id}");
        }
    }
}
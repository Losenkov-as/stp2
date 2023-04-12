using BlazorApp.Models;
using BlazorApp.Models.Account;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IBuildService
    {
        Build Build { get; }
        Task Initialize();
        Task Create(AddBuild model);
        Task<IList<Build>> GetAll();
        Task<Build> GetById(string id);
        Task Update(string id, EditBuild model);
        Task Delete(string id);
    }

    public class BuildService : IBuildService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _BuildKey = "Build";

        public Build Build { get; private set; }

        public BuildService(
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
            Build = await _localStorageService.GetItem<Build>(_BuildKey);
        }


        public async Task Create(AddBuild model)
        {
            await _httpService.Post("/builds/create", model);
        }

        public async Task<IList<Build>> GetAll()
        {
            return await _httpService.Get<IList<Build>>("/builds");
        }

        public async Task<Build> GetById(string id)
        {
            return await _httpService.Get<Build>($"/builds/{id}");
        }

        public async Task Update(string id, EditBuild model)
        {
            await _httpService.Put($"/builds/{id}", model);

            // update stored Build if the logged in Build updated their own record
            if (id == Build.Id.ToString()) 
            {
                // update local storage
                Build.Name = model.Name;
                await _localStorageService.SetItem(_BuildKey, Build);
            }
        }

        public async Task Delete(string id)
        {
            await _httpService.Delete($"/builds/{id}");
        }
    }
}
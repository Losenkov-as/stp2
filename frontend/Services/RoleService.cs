using BlazorApp.Models;
using BlazorApp.Models.Account;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IRoleService
    {
        Role Role { get; }
        Task Initialize();
        Task Create(AddRole model);
        Task<IList<Role>> GetAll();
        Task<Role> GetById(string id);
        Task Update(string id, EditRole model);
        Task Delete(string id);
    }

    public class RoleService : IRoleService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _roleKey = "role";

        public Role Role { get; private set; }

        public RoleService(
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
            Role = await _localStorageService.GetItem<Role>(_roleKey);
        }


        public async Task Create(AddRole model)
        {
            await _httpService.Post("/roles/create", model);
        }

        public async Task<IList<Role>> GetAll()
        {
            return await _httpService.Get<IList<Role>>("/roles");
        }

        public async Task<Role> GetById(string id)
        {
            return await _httpService.Get<Role>($"/roles/{id}");
        }

        public async Task Update(string id, EditRole model)
        {
            await _httpService.Put($"/roles/{id}", model);

            // update stored role if the logged in role updated their own record
            if (id == Role.Id) 
            {
                // update local storage
                Role.Name = model.Name;
                await _localStorageService.SetItem(_roleKey, Role);
            }
        }

        public async Task Delete(string id)
        {
            await _httpService.Delete($"/roles/{id}");
        }
    }
}
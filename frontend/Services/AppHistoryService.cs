using BlazorApp.Models;
using BlazorApp.Models.Account;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IAppHistoryService
    {
        AppHistory AppHistory { get; }
        Task Initialize();
        Task Create(AddAppHistory model);
        Task<IList<AppHistory>> GetAll();
        Task<AppHistory> GetById(string id);
        Task Delete(string id);
    }

    public class AppHistoryService : IAppHistoryService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _AppHistoryKey = "AppHistory";

        public AppHistory AppHistory { get; private set; }

        public AppHistoryService(
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
            AppHistory = await _localStorageService.GetItem<AppHistory>(_AppHistoryKey);
        }


        public async Task Create(AddAppHistory model)
        {
            await _httpService.Post("/AppHistory/create", model);
        }

        public async Task<IList<AppHistory>> GetAll()
        {
            return await _httpService.Get<IList<AppHistory>>("/AppHistory");
        }

        public async Task<AppHistory> GetById(string id)
        {
            return await _httpService.Get<AppHistory>($"/AppHistory/{id}");
        }

       

        public async Task Delete(string id)
        {
            await _httpService.Delete($"/AppHistory/{id}");
        }
    }
}
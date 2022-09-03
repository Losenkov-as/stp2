using BlazorApp.Models;
using BlazorApp.Models.Account;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface IStatusService
    {
        Status Status { get; }
        Task Initialize();
        Task Create(AddStatus model);
        Task<IList<Status>> GetAll();
        Task<Status> GetById(string id);
        Task Update(string id, EditStatus model);
        Task Delete(string id);
    }

    public class StatusService : IStatusService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _statusKey = "status";

        public Status Status { get; private set; }

        public StatusService(
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
            Status = await _localStorageService.GetItem<Status>(_statusKey);
        }


        public async Task Create(AddStatus model)
        {
            await _httpService.Post("/statuses/create", model);
        }

        public async Task<IList<Status>> GetAll()
        {
            return await _httpService.Get<IList<Status>>("/statuses");
        }

        public async Task<Status> GetById(string id)
        {
            return await _httpService.Get<Status>($"/statuses/{id}");
        }

        public async Task Update(string id, EditStatus model)
        {
            await _httpService.Put($"/statuses/{id}", model);

            // update stored Status if the logged in Status updated their own record
            if (id == Status.Id) 
            {
                // update local storage
                Status.Name = model.Name;
                await _localStorageService.SetItem(_statusKey, Status);
            }
        }

        public async Task Delete(string id)
        {
            await _httpService.Delete($"/statuses/{id}");
        }
    }
}
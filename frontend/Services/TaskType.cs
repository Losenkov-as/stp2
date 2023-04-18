using BlazorApp.Models;
using BlazorApp.Models.Account;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public interface ITaskTypeService
    {
        TaskType TaskType { get; }
        Task Initialize();
        Task Create(AddTaskType model);
        Task<IList<TaskType>> GetAll();
        Task<TaskType> GetById(string id);
        Task Update(string id, EditTaskType model);
        Task Delete(string id);
    }

    public class TaskTypeService : ITaskTypeService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _TaskTypeKey = "TaskType";

        public TaskType TaskType { get; private set; }

        public TaskTypeService(
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
            TaskType = await _localStorageService.GetItem<TaskType>(_TaskTypeKey);
        }


        public async Task Create(AddTaskType model)
        {
            await _httpService.Post("/TaskType/create", model);
        }

        public async Task<IList<TaskType>> GetAll()
        {
            return await _httpService.Get<IList<TaskType>>("/TaskType");
        }

        public async Task<TaskType> GetById(string id)
        {
            return await _httpService.Get<TaskType>($"/TaskType/{id}");
        }

        public async Task Update(string id, EditTaskType model)
        {
            await _httpService.Put($"/TaskType/{id}", model);

            // update stored TaskType if the logged in TaskType updated their own record
            if (id == TaskType.Id.ToString()) 
            {
                // update local storage
                TaskType.Name = model.Name;
                await _localStorageService.SetItem(_TaskTypeKey, TaskType);
            }
        }

        public async Task Delete(string id)
        {
            await _httpService.Delete($"/TaskType/{id}");
        }
    }
}
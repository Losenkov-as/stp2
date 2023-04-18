using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.TaskType;
using WebApi.Services;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TaskTypeController : ControllerBase
    {
        private ITaskTypeService _TaskTypeService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public TaskTypeController(
            ITaskTypeService TaskTypeervice,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _TaskTypeService = TaskTypeervice;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        //[AllowAnonymous]                                                      Авторизация для места не нужна
        //[HttpPost("authenticate")]
        //public IActionResult Authenticate(AuthenticateRequest model)
        //{
        //    var response = _userService.Authenticate(model);
        //    return Ok(response);
        //}

        [AllowAnonymous]
        [HttpPost("create")]
        public IActionResult Register(RegisterRequest model)                    // Если называется регистрация а по факту добавление всё гуд?
        {
            _TaskTypeService.Register(model);
            return Ok(new { message = "Registration successful" });
        }

        [HttpGet]
        public IActionResult GetAll()                                        // чтобы запустить этот метод нужно смотреть в TaskTypeervice.cs стр 52-56
        {
            var TaskType = _TaskTypeService.GetAll();
            return Ok(TaskType);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Room = _TaskTypeService.GetById(id);
            return Ok(Room);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            _TaskTypeService.Update(id, model);
            return Ok(new { message = "Информация о местоположении успешно обновлена" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _TaskTypeService.Delete(id);
            return Ok(new { message = "Местоположение успешно удалено" });      // Возможно этот метод не нужен для помещений
        }
    }
}

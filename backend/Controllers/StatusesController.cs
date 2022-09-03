using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.Statuses;
using WebApi.Services;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StatusesController : ControllerBase
    {
        private IStatusService _statusService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public StatusesController(
            IStatusService statusService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _statusService = statusService;
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
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)                    // Если называется регистрация а по факту добавление всё гуд?
        {
            _statusService.Register(model);
            return Ok(new { message = "Registration successful" });
        }

        [HttpGet]
        public IActionResult GetAll()                                        // чтобы запустить этот метод нужно смотреть в StatusService.cs стр 52-56
        {
            var statuses = _statusService.GetAll();
            return Ok(statuses);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var status = _statusService.GetById(id);
            return Ok(status);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            _statusService.Update(id, model);
            return Ok(new { message = "Информация о местоположении успешно обновлена" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _statusService.Delete(id);
            return Ok(new { message = "Местоположение успешно удалено" });     
        }
    }
}

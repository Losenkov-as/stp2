using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.MachineModel;
using WebApi.Services;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MachineModelsController : ControllerBase
    {
        private IMachineModelService _MachineModelService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public MachineModelsController(
            IMachineModelService MachineModelService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _MachineModelService = MachineModelService;
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
            _MachineModelService.Register(model);
            return Ok(new { message = "Registration successful" });
        }

        [HttpGet]
        public IActionResult GetAll()                                        // чтобы запустить этот метод нужно смотреть в MachineModelService.cs стр 52-56
        {
            var MachineModels = _MachineModelService.GetAll();
            return Ok(MachineModels);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var MachineModel = _MachineModelService.GetById(id);
            return Ok(MachineModel);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            _MachineModelService.Update(id, model);
            return Ok(new { message = "Информация о местоположении успешно обновлена" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _MachineModelService.Delete(id);
            return Ok(new { message = "Местоположение успешно удалено" });      // Возможно этот метод не нужен для помещений
        }
    }
}

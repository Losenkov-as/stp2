using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.MachineType;
using WebApi.Services;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class MachineTypesController : ControllerBase
    {
        private IMachineTypeService _MachineTypeService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public MachineTypesController(
            IMachineTypeService MachineTypeService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _MachineTypeService = MachineTypeService;
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
            _MachineTypeService.Register(model);
            return Ok(new { message = "Registration successful" });
        }

        [HttpGet]
        public IActionResult GetAll()                                        // чтобы запустить этот метод нужно смотреть в MachineTypeService.cs стр 52-56
        {
            var MachineTypes = _MachineTypeService.GetAll();
            return Ok(MachineTypes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var MachineType = _MachineTypeService.GetById(id);
            return Ok(MachineType);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            _MachineTypeService.Update(id, model);
            return Ok(new { message = "Информация о местоположении успешно обновлена" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _MachineTypeService.Delete(id);
            return Ok(new { message = "Местоположение успешно удалено" });      // Возможно этот метод не нужен для помещений
        }
    }
}

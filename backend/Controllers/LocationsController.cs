using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.Locations;
using WebApi.Services;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private ILocationService _locationService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public LocationsController(
            ILocationService locationService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _locationService = locationService;
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
            _locationService.Register(model);
            return Ok(new { message = "Registration successful" });
        }

        [HttpGet]
        public IActionResult GetAll()                                        // чтобы запустить этот метод нужно смотреть в LocationService.cs стр 52-56
        {
            var locations = _locationService.GetAll();
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var location = _locationService.GetById(id);
            return Ok(location);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            _locationService.Update(id, model);
            return Ok(new { message = "Информация о местоположении успешно обновлена" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _locationService.Delete(id);
            return Ok(new { message = "Местоположение успешно удалено" });      // Возможно этот метод не нужен для помещений
        }
    }
}

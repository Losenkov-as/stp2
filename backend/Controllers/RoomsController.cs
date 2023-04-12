using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.Rooms;
using WebApi.Services;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RoomsController : ControllerBase
    {
        private IRoomService _RoomService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public RoomsController(
            IRoomService RoomService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _RoomService = RoomService;
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
            _RoomService.Register(model);
            return Ok(new { message = "Registration successful" });
        }

        [HttpGet]
        public IActionResult GetAll()                                        // чтобы запустить этот метод нужно смотреть в RoomService.cs стр 52-56
        {
            var Rooms = _RoomService.GetAll();
            return Ok(Rooms);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Room = _RoomService.GetById(id);
            return Ok(Room);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            _RoomService.Update(id, model);
            return Ok(new { message = "Информация о местоположении успешно обновлена" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _RoomService.Delete(id);
            return Ok(new { message = "Местоположение успешно удалено" });      // Возможно этот метод не нужен для помещений
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.Builds;
using WebApi.Services;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BuildsController : ControllerBase
    {
        private IBuildService _BuildService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public BuildsController(
            IBuildService BuildService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _BuildService = BuildService;
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
            _BuildService.Register(model);
            return Ok(new { message = "Registration successful" });
        }

        [HttpGet]
        public IActionResult GetAll()                                        // чтобы запустить этот метод нужно смотреть в BuildService.cs стр 52-56
        {
            var Builds = _BuildService.GetAll();
            return Ok(Builds);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Build = _BuildService.GetById(id);
            return Ok(Build);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            _BuildService.Update(id, model);
            return Ok(new { message = "Информация о местоположении успешно обновлена" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _BuildService.Delete(id);
            return Ok(new { message = "Местоположение успешно удалено" });      // Возможно этот метод не нужен для помещений
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.AppHistory;
using WebApi.Services;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AppHistorysController : ControllerBase
    {
        private IAppHistoryService _AppHistoryService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AppHistorysController(
            IAppHistoryService AppHistoryService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _AppHistoryService = AppHistoryService;
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
            _AppHistoryService.Register(model);
            return Ok(new { message = "Registration successful" });
        }

        [HttpGet]
        public IActionResult GetAll()                                        // чтобы запустить этот метод нужно смотреть в AppHistoryService.cs стр 52-56
        {
            var AppHistorys = _AppHistoryService.GetAll();
            return Ok(AppHistorys);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var AppHistory = _AppHistoryService.GetById(id);
            return Ok(AppHistory);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _AppHistoryService.Delete(id);
            return Ok(new { message = "Местоположение успешно удалено" });      // Возможно этот метод не нужен для помещений
        }
    }
}

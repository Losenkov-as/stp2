using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.Maintenance;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MaintenancesController : ControllerBase
    {
        private IMaintenanceService _maintenanceService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public MaintenancesController(
            IMaintenanceService maintenanceService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _maintenanceService = maintenanceService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public IActionResult Register(RegisterRequest model)
        {
            _maintenanceService.Register(model);
            return Ok(new { message = "Регистрация успешна" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var maintenances = _maintenanceService.GetAll();
            return Ok(maintenances);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var maintenance = _maintenanceService.GetById(id);
            return Ok(maintenance);
        }

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, UpdateRequest model)
        //{
        //    _maintenanceService.Update(id, model);
        //    return Ok(new { message = "Maintenance updated successfully" });
        //}

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _maintenanceService.Delete(id);
            return Ok(new { message = "Maintenance deleted successfully" });
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private IRoleService _roleService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public RolesController(
            IRoleService roleService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _roleService = roleService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }


        [AllowAnonymous]
        [HttpPost("create")]
        public IActionResult Register(Role model)
        {
            _roleService.Create(model);
            return Ok(new { message = "Registration successful" });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _roleService.GetAll();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var role = _roleService.GetById(id);
            return Ok(role);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Role model)
        {
            _roleService.Update(id, model);
            return Ok(new { message = "Role updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _roleService.Delete(id);
            return Ok(new { message = "Role deleted successfully" });
        }
    }
}

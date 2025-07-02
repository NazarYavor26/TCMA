using Microsoft.AspNetCore.Mvc;
using TCMA.BLL.Services;

namespace TCMA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly IComponentService _componentService;

        public ComponentController(IComponentService componentService)
        {
            _componentService = componentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string? searchComponent = null)
        {
            var components = await _componentService.GetAllAsync(searchComponent);
            return Ok(components);
        }
    }
}

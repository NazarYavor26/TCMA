using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TCMA.BLL.Models;
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
        [EnableRateLimiting("readOperations")]
        [ResponseCache(CacheProfileName = "Cache5Mins")]
        public async Task<IActionResult> Get(string? searchComponent = null)
        {
            var components = await _componentService.GetAllAsync(searchComponent);
            return Ok(components);
        }

        [HttpGet("{id}")]
        [EnableRateLimiting("readOperations")]
        [ResponseCache(CacheProfileName = "Cache5Mins")]
        public async Task<IActionResult> Get(int id)
        {
            var component = await _componentService.GetByIdAsync(id);
            return Ok(component);
        }

        [HttpPost]
        [EnableRateLimiting("writeOperations")]
        public async Task<IActionResult> Create([FromBody] ComponentSaveModel createComponentModel)
        {
            var createdComponent = await _componentService.CreateAsync(createComponentModel);
            return Ok(createdComponent);
        }

        [HttpPut("{id}")]
        [EnableRateLimiting("writeOperations")]
        public async Task<IActionResult> Update(int id, [FromBody] ComponentSaveModel updateComponentModel)
        {
            var updatedComponent = await _componentService.UpdateAsync(id, updateComponentModel);
            return Ok(updatedComponent);
        }

        [HttpDelete("{id}")]
        [EnableRateLimiting("writeOperations")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _componentService.DeleteAsync(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

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
        public async Task<IActionResult> Get([FromQuery] ComponentFilterModel filter)
        {
            var components = await _componentService.GetAllAsync(filter);
            return Ok(components);
        }

        [HttpGet("{id}")]
        [EnableRateLimiting("readOperations")]
        public async Task<IActionResult> Get(int id)
        {
            var component = await _componentService.GetByIdAsync(id);
            return Ok(component);
        }

        [HttpPost]
        [EnableRateLimiting("writeOperations")]
        public async Task<IActionResult> Create([FromBody] ComponentCreateModel componentCreate)
        {
            var createdComponent = await _componentService.CreateAsync(componentCreate);
            return Ok(createdComponent);
        }

        [HttpPut("{id}")]
        [EnableRateLimiting("writeOperations")]
        public async Task<IActionResult> Update(int id, [FromBody] ComponentUpdateModel componentUpdate)
        {
            var updatedComponent = await _componentService.UpdateAsync(id, componentUpdate);
            return Ok(updatedComponent);
        }

        [HttpPut("{id}/quantity")]
        [EnableRateLimiting("writeOperations")]
        public async Task<IActionResult> UpdateQuantity(int id, [FromQuery] QuantityUpdateModel quantityUpdate)
        {
            var updatedComponent = await _componentService.UpdateQuantityAsync(id, quantityUpdate);
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

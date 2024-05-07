using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/Property")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        public PropertiesController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPropertyList()
        {
            var DetailsList = await _propertyService.GetAllProperties();
            if (DetailsList == null)
            {
                return NotFound();
            }
            return Ok(DetailsList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPropertyById(int Id)
        {
            var propertyDetails = await _propertyService.GetPropertyById(Id);

            if (propertyDetails != null)
            {
                return Ok(propertyDetails);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty(PropertyDetails propertyDetails)
        {
            var isPropertyCreated = await _propertyService.CreateProperty(propertyDetails);

            if (isPropertyCreated)
            {
                return Ok(isPropertyCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProperty(PropertyDetails propertyDetails)
        {
            if (propertyDetails != null)
            {
                var isPropertyUpdated = await _propertyService.UpdateProperty(propertyDetails);
                if (isPropertyUpdated)
                {
                    return Ok(isPropertyUpdated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProperty(int Id)
        {
            var isPropertyDeleted = await _propertyService.DeleteProperty(Id);

            if (isPropertyDeleted)
            {
                return Ok(isPropertyDeleted);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

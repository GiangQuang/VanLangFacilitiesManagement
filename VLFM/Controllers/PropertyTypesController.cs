using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/PropertyType")]
    [ApiController]
    public class PropertyTypesController : ControllerBase
    {
        private readonly IPropertyTypeService _propertyTypeService;
        public PropertyTypesController(IPropertyTypeService propertyTypeService)
        {
            _propertyTypeService = propertyTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPropTypeList()
        {
            var DetailsList = await _propertyTypeService.GetAllPropTypes();
            if (DetailsList == null)
            {
                return NotFound();
            }
            return Ok(DetailsList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPropTypeById(int Id)
        {
            var propTypeDetails = await _propertyTypeService.GetPropTypeById(Id);

            if (propTypeDetails != null)
            {
                return Ok(propTypeDetails);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePropType(PropertyTypeDetails propertyTypeDetails)
        {
            var isPropTypeCreated = await _propertyTypeService.CreatePropType(propertyTypeDetails);

            if (isPropTypeCreated)
            {
                return Ok(isPropTypeCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePropType(PropertyTypeDetails propertyTypeDetails)
        {
            if (propertyTypeDetails != null)
            {
                var isPropTypeUpdated = await _propertyTypeService.UpdatePropType(propertyTypeDetails);
                if (isPropTypeUpdated)
                {
                    return Ok(isPropTypeUpdated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePropType(int Id)
        {
            var isPropTypeDeleted = await _propertyTypeService.DeletePropType(Id);

            if (isPropTypeDeleted)
            {
                return Ok(isPropTypeDeleted);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/propertytype")]
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
            string PropTypeID = HttpContext.Request.Query.ContainsKey("PropTypeID") ? HttpContext.Request.Query["PropTypeID"].ToString() : null;
            string PropTypename = HttpContext.Request.Query.ContainsKey("PropTypename") ? HttpContext.Request.Query["PropTypename"].ToString() : null;
            string Note = HttpContext.Request.Query.ContainsKey("Note") ? HttpContext.Request.Query["Note"].ToString() : null;

            if (!string.IsNullOrEmpty(PropTypeID))
            {
                DetailsList = DetailsList.Where(p => p.PropTypeID.Contains(PropTypeID, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(PropTypename))
            {
                DetailsList = DetailsList.Where(p => p.PropTypename.Contains(PropTypename, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Note))
            {
                DetailsList = DetailsList.Where(p => p.Note.Contains(Note, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var responseData = new
            {
                data = DetailsList,
            };

            return Ok(responseData);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPropTypeById(int Id)
        {
            var propTypeDetails = await _propertyTypeService.GetPropTypeById(Id);

            if (propTypeDetails != null)
            {
                var responseData = new { data = propTypeDetails };
                return Ok(responseData);
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

        [HttpPost("{Id}")]
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

        [HttpDelete]
        public async Task<IActionResult> DeletePropType(List<PropertyTypeDetails> propties)
        {
            var isPropTypeDeleted = await _propertyTypeService.DeletePropType(propties);

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

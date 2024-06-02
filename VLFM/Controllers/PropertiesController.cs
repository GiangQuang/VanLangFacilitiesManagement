using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Core.Response;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/property")]
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
            string PropertyID = HttpContext.Request.Query.ContainsKey("PropertyID") ? HttpContext.Request.Query["PropertyID"].ToString() : null;
            string PropTypeID = HttpContext.Request.Query.ContainsKey("PropTypeID") ? HttpContext.Request.Query["PropTypeID"].ToString() : null;
            string Propertycode = HttpContext.Request.Query.ContainsKey("Propertycode") ? HttpContext.Request.Query["Propertycode"].ToString() : null;
            string Propertyname = HttpContext.Request.Query.ContainsKey("Propertyname") ? HttpContext.Request.Query["Propertyname"].ToString() : null;
            string Unit = HttpContext.Request.Query.ContainsKey("Unit") ? HttpContext.Request.Query["Unit"].ToString() : null;
            string Note = HttpContext.Request.Query.ContainsKey("Note") ? HttpContext.Request.Query["Note"].ToString() : null;
            if (!string.IsNullOrEmpty(PropertyID))
            {
                DetailsList = DetailsList.Where(p => p.PropertyID.Contains(PropertyID, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(PropTypeID))
            {
                DetailsList = DetailsList.Where(p => p.PropTypeID.ToString().Contains(PropTypeID, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Propertycode))
            {
                DetailsList = DetailsList.Where(p => p.Propertycode.Contains(Propertycode, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Propertyname))
            {
                DetailsList = DetailsList.Where(p => p.Propertyname.Contains(Propertyname, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Unit))
            {
                DetailsList = DetailsList.Where(p => p.Unit.Contains(Unit, StringComparison.OrdinalIgnoreCase)).ToList();
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
        public async Task<IActionResult> GetPropertyById(int Id)
        {
            var propertyDetails = await _propertyService.GetPropertyById(Id);

            if (propertyDetails != null)
            {
                var responseData = new
                {
                    data = propertyDetails,
                };
                return Ok(responseData);
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

        [HttpPost("{Id}")]
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

        [HttpDelete]
        public async Task<IActionResult> DeleteProperty(List<PropertyResponse> proper)
        {
            var isPropertyDeleted = await _propertyService.DeleteProperty(proper);

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

using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/provider")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly IProviderService _providerService;
        public ProvidersController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProviderList()
        {
            var DetailsList = await _providerService.GetAllProviders();
            if (DetailsList == null)
            {
                return NotFound();
            }
            string ProviderID = HttpContext.Request.Query.ContainsKey("ProviderID") ? HttpContext.Request.Query["ProviderID"].ToString() : null;
            string Providername = HttpContext.Request.Query.ContainsKey("Providername") ? HttpContext.Request.Query["Providername"].ToString() : null;
            string Address = HttpContext.Request.Query.ContainsKey("Address") ? HttpContext.Request.Query["Address"].ToString() : null;
            string Note = HttpContext.Request.Query.ContainsKey("Note") ? HttpContext.Request.Query["Note"].ToString() : null;

            if (!string.IsNullOrEmpty(ProviderID))
            {
                DetailsList = DetailsList.Where(p => p.ProviderID.Contains(ProviderID, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Providername))
            {
                DetailsList = DetailsList.Where(p => p.Providername.Contains(Providername, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Address))
            {
                DetailsList = DetailsList.Where(p => p.Address.Contains(Address, StringComparison.OrdinalIgnoreCase)).ToList();
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
        public async Task<IActionResult> GetProviderById(int Id)
        {
            var providerDetails = await _providerService.GetProviderById(Id);

            if (providerDetails != null)
            {
                var responseData = new
                {
                    data = providerDetails,
                };
                return Ok(responseData);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProvider(ProviderDetails providerDetails)
        {
            var isProviderCreated = await _providerService.CreateProvider(providerDetails);

            if (isProviderCreated)
            {
                return Ok(isProviderCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{Id}")]
        public async Task<IActionResult> UpdateProvider(ProviderDetails providerDetails)
        {
            if (providerDetails != null)
            {
                var isProviderUpdated = await _providerService.UpdateProvider(providerDetails);
                if (isProviderUpdated)
                {
                    return Ok(isProviderUpdated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProvider(List<ProviderDetails> prov)
        {
            var isProviderDeleted = await _providerService.DeleteProvider(prov);

            if (isProviderDeleted)
            {
                return Ok(isProviderDeleted);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

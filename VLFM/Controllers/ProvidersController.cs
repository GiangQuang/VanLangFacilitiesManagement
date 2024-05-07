using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/Provider")]
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
            return Ok(DetailsList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProviderById(int Id)
        {
            var providerDetails = await _providerService.GetProviderById(Id);

            if (providerDetails != null)
            {
                return Ok(providerDetails);
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

        [HttpPut]
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

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProvider(int Id)
        {
            var isProviderDeleted = await _providerService.DeleteProvider(Id);

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

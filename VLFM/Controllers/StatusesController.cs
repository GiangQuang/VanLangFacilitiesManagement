using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/Status")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly IStatusService _statusService;
        public StatusesController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatusList()
        {
            var DetailsList = await _statusService.GetAllStatuses();
            if (DetailsList == null)
            {
                return NotFound();
            }
            return Ok(DetailsList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetStatusById(int Id)
        {
            var statusDetails = await _statusService.GetStatusById(Id);

            if (statusDetails != null)
            {
                return Ok(statusDetails);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatus(StatusDetails statusDetails)
        {
            var isStatusCreated = await _statusService.CreateStatus(statusDetails);

            if (isStatusCreated)
            {
                return Ok(isStatusCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStatus(StatusDetails statusDetails)
        {
            if (statusDetails != null)
            {
                var isStatusUpdated = await _statusService.UpdateStatus(statusDetails);
                if (isStatusUpdated)
                {
                    return Ok(isStatusUpdated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteStatus(int Id)
        {
            var isStatusDeleted = await _statusService.DeleteStatus(Id);

            if (isStatusDeleted)
            {
                return Ok(isStatusDeleted);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

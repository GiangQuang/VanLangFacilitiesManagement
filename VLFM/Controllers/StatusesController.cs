using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/status")]
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
            string StatusID = HttpContext.Request.Query.ContainsKey("StatusID") ? HttpContext.Request.Query["StatusID"].ToString() : null;
            string Statusname = HttpContext.Request.Query.ContainsKey("Statusname") ? HttpContext.Request.Query["Statusname"].ToString() : null;
            string Note = HttpContext.Request.Query.ContainsKey("Note") ? HttpContext.Request.Query["Note"].ToString() : null;

            if (!string.IsNullOrEmpty(StatusID))
            {
                DetailsList = DetailsList.Where(s => s.StatusID.Contains(StatusID, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Statusname))
            {
                DetailsList = DetailsList.Where(s => s.Statusname.Contains(Statusname, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Note))
            {
                DetailsList = DetailsList.Where(s => s.Note.Contains(Note, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var responseData = new
            {
                data = DetailsList,
            };
            return Ok(responseData);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetStatusById(int Id)
        {
            var statusDetails = await _statusService.GetStatusById(Id);

            if (statusDetails != null)
            {
                var responseData = new
                {
                    data = statusDetails,
                };
                return Ok(responseData);
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

        [HttpPost("{Id}")]
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

        [HttpDelete]
        public async Task<IActionResult> DeleteStatus(List<StatusDetails> sta)
        {
            var isStatusDeleted = await _statusService.DeleteStatus(sta);

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

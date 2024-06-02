using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/propose")]
    [ApiController]
    public class ProposesController : ControllerBase
    {
        private readonly IProposeService _proposeService;
        public ProposesController(IProposeService proposeService)
        {
            _proposeService = proposeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProposeList()
        {
            var DetailsList = await _proposeService.GetAllProposes();
            if (DetailsList == null)
            {
                return NotFound();
            }
            string ProposeID = HttpContext.Request.Query.ContainsKey("ProposeID") ? HttpContext.Request.Query["ProposeID"].ToString() : null;
            string Proposename = HttpContext.Request.Query.ContainsKey("Proposename") ? HttpContext.Request.Query["Proposename"].ToString() : null;
            string Note = HttpContext.Request.Query.ContainsKey("Note") ? HttpContext.Request.Query["Note"].ToString() : null;

            if (!string.IsNullOrEmpty(ProposeID))
            {
                DetailsList = DetailsList.Where(s => s.ProposeID.Contains(ProposeID, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Proposename))
            {
                DetailsList = DetailsList.Where(s => s.Proposename.Contains(Proposename, StringComparison.OrdinalIgnoreCase)).ToList();
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
        public async Task<IActionResult> GetProposeById(int Id)
        {
            var proposeDetails = await _proposeService.GetProposeById(Id);

            if (proposeDetails != null)
            {
                var responseData = new
                {
                    data = proposeDetails,
                };
                return Ok(responseData);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePropose(ProposeDetails proposeDetails)
        {
            var isProposeCreated = await _proposeService.CreatePropose(proposeDetails);

            if (isProposeCreated)
            {
                return Ok(isProposeCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{Id}")]
        public async Task<IActionResult> UpdatePropose(ProposeDetails proposeDetails)
        {
            if (proposeDetails != null)
            {
                var isProposeUpdated = await _proposeService.UpdatePropose(proposeDetails);
                if (isProposeUpdated)
                {
                    return Ok(isProposeUpdated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePropose(List<ProposeDetails> prop)
        {
            var isProposeDeleted = await _proposeService.DeletePropose(prop);

            if (isProposeDeleted)
            {
                return Ok(isProposeDeleted);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/Propose")]
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
            return Ok(DetailsList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProposeById(int Id)
        {
            var proposeDetails = await _proposeService.GetProposeById(Id);

            if (proposeDetails != null)
            {
                return Ok(proposeDetails);
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

        [HttpPut]
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

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePropose(int Id)
        {
            var isProposeDeleted = await _proposeService.DeletePropose(Id);

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

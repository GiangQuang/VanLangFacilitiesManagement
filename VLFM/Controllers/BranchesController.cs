using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/Branch")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _branchService;
        public BranchesController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBranchList()
        {
            var DetailsList = await _branchService.GetAllBranches();
            if (DetailsList == null)
            {
                return NotFound();
            }
            return Ok(DetailsList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBranchById(int Id)
        {
            var branchDetails = await _branchService.GetBranchById(Id);

            if (branchDetails != null)
            {
                return Ok(branchDetails);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBranch(BranchDetails branchDetails)
        {
            var isBranchCreated = await _branchService.CreateBranch(branchDetails);

            if (isBranchCreated)
            {
                return Ok(isBranchCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBranch(BranchDetails branchDetails)
        {
            if (branchDetails != null)
            {
                var isBranchUpdated = await _branchService.UpdateBranch(branchDetails);
                if (isBranchUpdated)
                {
                    return Ok(isBranchUpdated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteBranch(int Id)
        {
            var isBranchDeleted = await _branchService.DeleteBranch(Id);

            if (isBranchDeleted)
            {
                return Ok(isBranchDeleted);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/branch")]
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

            string BranchID = HttpContext.Request.Query.ContainsKey("BranchID") ? HttpContext.Request.Query["BranchID"].ToString() : null;
            string Branchname = HttpContext.Request.Query.ContainsKey("Branchname") ? HttpContext.Request.Query["Branchname"].ToString() : null;
            string Address = HttpContext.Request.Query.ContainsKey("Address") ? HttpContext.Request.Query["Address"].ToString() : null;

            if (!string.IsNullOrEmpty(Branchname))
            {
                DetailsList = DetailsList.Where(b => b.Branchname.Contains(Branchname, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(BranchID))
            {
                DetailsList = DetailsList.Where(b => b.BranchID.Contains(BranchID, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Address))
            {
                DetailsList = DetailsList.Where(b => b.Address.Contains(Address, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            var responseData = new
            {
                data = DetailsList,
            };

            return Ok(responseData);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBranchById(int Id)
        {
            var branchDetails = await _branchService.GetBranchById(Id);

            if (branchDetails != null)
            {
                var responseData = new { data = branchDetails };
                return Ok(responseData);
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

        [HttpPost("{Id}")]
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

        [HttpDelete]
        public async Task<IActionResult> DeleteBranch(List<BranchDetails> brs)
        {
            var isBranchDeleted = await _branchService.DeleteBranch(brs);

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

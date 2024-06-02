using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Core.Response;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeptList()
        {
            var DetailsList = await _departmentService.GetAllDepartments();
            if (DetailsList == null)
            {
                return NotFound();
            }
            string BranchID = HttpContext.Request.Query.ContainsKey("BranchID") ? HttpContext.Request.Query["BranchID"].ToString() : null;
            string DeptID = HttpContext.Request.Query.ContainsKey("DeptID") ? HttpContext.Request.Query["DeptID"].ToString() : null;
            string Deptname = HttpContext.Request.Query.ContainsKey("Deptname") ? HttpContext.Request.Query["Deptname"].ToString() : null;
            string Note = HttpContext.Request.Query.ContainsKey("Note") ? HttpContext.Request.Query["Note"].ToString() : null;

            if (!string.IsNullOrEmpty(BranchID))
            {
                DetailsList = DetailsList.Where(d => d.BranchID.ToString().Contains(BranchID, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(DeptID))
            {
                DetailsList = DetailsList.Where(d => d.DeptID.Contains(DeptID, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Deptname))
            {
                DetailsList = DetailsList.Where(d => d.Deptname.Contains(Deptname, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Note))
            {
                DetailsList = DetailsList.Where(d => d.Note.Contains(Note, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var responseData = new
            {
                data = DetailsList,
            };

            return Ok(responseData);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetDeptById(int Id)
        {
            var deptDetails = await _departmentService.GetDepartmentById(Id);

            if (deptDetails != null)
            {
                var responseData = new { data = deptDetails };
                return Ok(responseData);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDept(DepartmentDetails departmentDetails)
        {
            var isDeptCreated = await _departmentService.CreateDepartment(departmentDetails);

            if (isDeptCreated)
            {
                return Ok(isDeptCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{Id}")]
        public async Task<IActionResult> UpdateDept(DepartmentDetails departmentDetails)
        {
            if (departmentDetails != null)
            {
                var isDeptUpdated = await _departmentService.UpdateDepartment(departmentDetails);
                if (isDeptUpdated)
                {
                    return Ok(isDeptUpdated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDept(List<DepartmentResponse> depts)
        {
            var isDeptDeleted = await _departmentService.DeleteDepartment(depts);

            if (isDeptDeleted)
            {
                return Ok(isDeptDeleted);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

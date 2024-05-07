using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/Department")]
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
            return Ok(DetailsList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetDeptById(int Id)
        {
            var deptDetails = await _departmentService.GetDepartmentById(Id);

            if (deptDetails != null)
            {
                return Ok(deptDetails);
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

        [HttpPut]
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

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteDept(int Id)
        {
            var isDeptDeleted = await _departmentService.DeleteDepartment(Id);

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

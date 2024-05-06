using Microsoft.AspNetCore.Mvc;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeList()
        {
            var DetailsList = await _employeeService.GetAllEmployees();
            if (DetailsList == null)
            {
                return NotFound();
            }
            return Ok(DetailsList);
        }

        [HttpGet("{IDNV}")]
        public async Task<IActionResult> GetEmployeerById(int IDNV)
        {
            var employeeDetails = await _employeeService.GetEmployeeById(IDNV);

            if (employeeDetails != null)
            {
                return Ok(employeeDetails);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeDetails employeeDetails)
        {
            var isEmployeeCreated = await _employeeService.CreateEmployee(employeeDetails);

            if (isEmployeeCreated)
            {
                return Ok(isEmployeeCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(EmployeeDetails employeeDetails)
        {
            if (employeeDetails != null)
            {
                var isEmployeeUpdated = await _employeeService.UpdateEmployee(employeeDetails);
                if (isEmployeeUpdated)
                {
                    return Ok(isEmployeeUpdated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{IDNV}")]
        public async Task<IActionResult> DeleteEmployee(int IDNV)
        {
            var isEmployeeDeleted = await _employeeService.DeleteEmployee(IDNV);

            if (isEmployeeDeleted)
            {
                return Ok(isEmployeeDeleted);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

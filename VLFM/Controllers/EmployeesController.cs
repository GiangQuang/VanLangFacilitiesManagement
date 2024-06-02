using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using VLFM.Core.Models;
using VLFM.Services;
using VLFM.Services.Interfaces;

namespace VLFM.Controllers
{
    [Route("api/employee")]
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
            string EmployeeID = HttpContext.Request.Query.ContainsKey("EmployeeID") ? HttpContext.Request.Query["EmployeeID"].ToString() : null;
            string Employeename = HttpContext.Request.Query.ContainsKey("Employeename") ? HttpContext.Request.Query["Employeename"].ToString() : null;
            string Phonenumber = HttpContext.Request.Query.ContainsKey("Phonenumber") ? HttpContext.Request.Query["Phonenumber"].ToString() : null;
            string Dateofbirth = HttpContext.Request.Query.ContainsKey("Dateofbirth") ? HttpContext.Request.Query["Dateofbirth"].ToString() : null;
            string Address = HttpContext.Request.Query.ContainsKey("Address") ? HttpContext.Request.Query["Address"].ToString() : null;
            string Status = HttpContext.Request.Query.ContainsKey("Status") ? HttpContext.Request.Query["Status"].ToString() : null;
            if (!string.IsNullOrEmpty(EmployeeID))
            {
                DetailsList = DetailsList.Where(e => e.EmployeeID.Contains(EmployeeID, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Employeename))
            {
                DetailsList = DetailsList.Where(e => e.Employeename.Contains(Employeename, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Phonenumber))
            {
                DetailsList = DetailsList.Where(e => e.Phonenumber.Contains(Phonenumber, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Dateofbirth))
            {
                DateTime parsedDateOfBirth;
                if (DateTime.TryParse(Dateofbirth, out parsedDateOfBirth))
                {
                    DetailsList = DetailsList.Where(e => e.Dateofbirth == parsedDateOfBirth).ToList();
                }
            }
            if (!string.IsNullOrEmpty(Address))
            {
                DetailsList = DetailsList.Where(e => e.Address.Contains(Address, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Status))
            {
                int parsedStatus;
                if (int.TryParse(Status, out parsedStatus))
                {
                    DetailsList = DetailsList.Where(e => e.Status == parsedStatus).ToList();
                }
            }

            var responseData = new
            {
                data = DetailsList,
            };

            return Ok(responseData);
        }

        [HttpGet("{IDNV}")]
        public async Task<IActionResult> GetEmployeerById(int IDNV)
        {
            var employeeDetails = await _employeeService.GetEmployeeById(IDNV);

            if (employeeDetails != null)
            {
                var responseData = new { data = employeeDetails };
                return Ok(responseData);
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

        [HttpPost("{IDNV}")]
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

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(List<EmployeeDetails> emps)
        {
            var isEmployeeDeleted = await _employeeService.DeleteEmployee(emps);

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Models;

namespace VLFM.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<bool> CreateEmployee(EmployeeDetails employeeDetails);
        Task<IEnumerable<EmployeeDetails>> GetAllEmployees();
        Task<EmployeeDetails> GetEmployeeById(int IDNV);
        Task<bool> UpdateEmployee(EmployeeDetails employeeDetails);
        Task<bool> DeleteEmployee(List<EmployeeDetails> emps);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Models;

namespace VLFM.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<bool> CreateDepartment(DepartmentDetails departmentDetails);
        Task<IEnumerable<DepartmentDetails>> GetAllDepartments();
        Task<DepartmentDetails> GetDepartmentById(int Id);
        Task<bool> UpdateDepartment(DepartmentDetails departmentDetails);
        Task<bool> DeleteDepartment(int Id);
    }
}

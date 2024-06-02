using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Models;
using VLFM.Core.Response;

namespace VLFM.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<bool> CreateDepartment(DepartmentDetails departmentDetails);
        Task<IEnumerable<DepartmentResponse>> GetAllDepartments();
        Task<DepartmentResponse> GetDepartmentById(int Id);
        Task<bool> UpdateDepartment(DepartmentDetails departmentDetails);
        Task<bool> DeleteDepartment(List<DepartmentResponse> depts);
    }
}

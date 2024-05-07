using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Interfaces;
using VLFM.Core.Models;
using VLFM.Services.Interfaces;

namespace VLFM.Services
{
    public class DepartmentService : IDepartmentService
    {
        public IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateDepartment(DepartmentDetails departmentDetails)
        {
            if (departmentDetails != null)
            {
                departmentDetails.DeptID = GenerateDeptID();
                await _unitOfWork.Departments.Add(departmentDetails);
                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteDepartment(int Id)
        {
            if (Id > 0)
            {
                var departmentDetails = await _unitOfWork.Departments.GetById(Id);
                if (departmentDetails != null)
                {
                    _unitOfWork.Departments.Delete(departmentDetails);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<DepartmentDetails>> GetAllDepartments()
        {
            var DetailsList = await _unitOfWork.Departments.GetAll();
            return DetailsList;
        }

        public async Task<DepartmentDetails> GetDepartmentById(int Id)
        {
            if (Id > 0)
            {
                var departmentDetails = await _unitOfWork.Departments.GetById(Id);
                if (departmentDetails != null)
                {
                    return departmentDetails;
                }
            }
            return null;
        }

        public async Task<bool> UpdateDepartment(DepartmentDetails departmentDetails)
        {
            if (departmentDetails != null)
            {
                var dept = await _unitOfWork.Departments.GetById(departmentDetails.Id);
                if (dept != null)
                {
                    dept.Deptname = departmentDetails.Deptname;
                    dept.Note = departmentDetails.Note;

                    _unitOfWork.Departments.Update(dept);

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        private string GenerateDeptID()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}

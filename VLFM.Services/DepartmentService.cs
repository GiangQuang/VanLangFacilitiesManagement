using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Interfaces;
using VLFM.Core.Models;
using VLFM.Core.Response;
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
                await _unitOfWork.Departments.Add(departmentDetails);
                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteDepartment(List<DepartmentResponse> depts)
        {
            if (depts != null && depts.Any())
            {
                foreach (var item in depts)
                {
                    var departmentDetails = await _unitOfWork.Departments.GetById(item.Id);
                    if (departmentDetails != null)
                    {
                        _unitOfWork.Departments.Delete(departmentDetails);
                    }
                }
                var result = _unitOfWork.Save();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<IEnumerable<DepartmentResponse>> GetAllDepartments()
        {
            try {
                var departments = await _unitOfWork.Departments.GetAll();
                var branches = await _unitOfWork.Branches.GetAll();

                var query = from de in departments
                            join br in branches on de.BranchID equals br.BranchID
                            select new DepartmentResponse
                            {
                                Id = de.Id,
                                DeptID = de.DeptID,
                                BranchID = de.BranchID,
                                Deptname = de.Deptname,
                                Note = de.Note,
                            };
                return query.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DepartmentResponse> GetDepartmentById(int Id)
        {
            if (Id > 0)
            {
                var department = await _unitOfWork.Departments.GetById(Id);
                var branches = await _unitOfWork.Branches.GetAll();

                if (department != null)
                {
                    try
                    {
                        var branch = branches.FirstOrDefault(br => br.BranchID == department.BranchID);
                        var response = new DepartmentResponse
                        {
                            Id = department.Id,
                            DeptID = department.DeptID,
                            BranchID = department.DeptID,
                            Deptname = department.Deptname,
                            Note = department.Note,
                        };
                        return response;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
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
                    dept.BranchID = departmentDetails.BranchID;
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
    }
}

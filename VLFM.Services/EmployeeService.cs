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
    public class EmployeeService : IEmployeeService
    {
        public IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateEmployee(EmployeeDetails employeeDetails)
        {
            if (employeeDetails != null)
            {
                employeeDetails.EmployeeID = GenerateEmployeeId();
                await _unitOfWork.Employees.Add(employeeDetails);
                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteEmployee(int IDNV)
        {
            if (IDNV > 0)
            {
                var employeeDetails = await _unitOfWork.Employees.GetById(IDNV);
                if (employeeDetails != null)
                {
                    _unitOfWork.Employees.Delete(employeeDetails);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<EmployeeDetails>> GetAllEmployees()
        {
            var DetailsList = await _unitOfWork.Employees.GetAll();
            return DetailsList;
        }

        public async Task<EmployeeDetails> GetEmployeeById(int IDNV)
        {
            if (IDNV > 0)
            {
                var employeeDetails = await _unitOfWork.Employees.GetById(IDNV);
                if (employeeDetails != null)
                {
                    return employeeDetails;
                }
            }
            return null;
        }

        public async Task<bool> UpdateEmployee(EmployeeDetails employeeDetails)
        {
            if (employeeDetails != null)
            {
                var employee = await _unitOfWork.Employees.GetById(employeeDetails.IDNV);
                if (employee != null)
                {
                    employee.Employeename = employeeDetails.Employeename;
                    employee.Phonenumber = employeeDetails.Phonenumber;
                    employee.Dateofbirth = employeeDetails.Dateofbirth;
                    employee.Address = employeeDetails.Address;
                    employee.Status = employeeDetails.Status;

                    _unitOfWork.Employees.Update(employee);

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        private string GenerateEmployeeId()
        {
            // Tạo EmployeeID theo định dạng "yyyyMMddHHmmss"
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}

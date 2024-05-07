﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Interfaces;

namespace VLFM.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        public IUserRepository Users { get; }
        public IEmployeeRepository Employees { get; }
        public IBranchRepository Branches { get; }
        public IDepartmentRepository Departments { get; }
        public IPropertyTypeRepository PropTypes { get; }

        public UnitOfWork(DataContext dataContext, 
            IUserRepository userRepository,
            IEmployeeRepository employeeRepository,
            IBranchRepository branchRepository,
            IDepartmentRepository departmentRepository,
            IPropertyTypeRepository propertyTypeRepository)
        {
            _dataContext = dataContext;
            Users = userRepository;
            Employees = employeeRepository;
            Branches = branchRepository;
            Departments = departmentRepository;
            PropTypes = propertyTypeRepository;
        }
        public int Save()
        {
            return _dataContext.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataContext.Dispose();
            }
        }
    }
}

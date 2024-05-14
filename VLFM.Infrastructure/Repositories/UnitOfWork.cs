using Microsoft.EntityFrameworkCore;
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
        public IPropertyRepository Properties { get; }
        public IStatusRepository Statuses { get; }
        public IProposeRepository Proposes { get; }
        public IProviderRepository Providers { get; }
        public IReceiptRepository Receipts { get; }
        public IDetailedReceiptRepository ReceiptsDetailed { get; }

        public UnitOfWork(DataContext dataContext, 
            IUserRepository userRepository,
            IEmployeeRepository employeeRepository,
            IBranchRepository branchRepository,
            IDepartmentRepository departmentRepository,
            IPropertyTypeRepository propertyTypeRepository,
            IPropertyRepository propertyRepository,
            IStatusRepository statusRepository,
            IProposeRepository proposeRepository,
            IProviderRepository providerRepository,
            IReceiptRepository receiptRepository,
            IDetailedReceiptRepository detailedReceiptRepository)
        {
            _dataContext = dataContext;
            Users = userRepository;
            Employees = employeeRepository;
            Branches = branchRepository;
            Departments = departmentRepository;
            PropTypes = propertyTypeRepository;
            Properties = propertyRepository;
            Statuses = statusRepository;
            Proposes = proposeRepository;
            Providers = providerRepository;
            Receipts = receiptRepository;
            ReceiptsDetailed = detailedReceiptRepository;
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

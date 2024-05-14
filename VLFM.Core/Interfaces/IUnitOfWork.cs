using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLFM.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IEmployeeRepository Employees { get; }
        IBranchRepository Branches { get; }
        IDepartmentRepository Departments { get; }
        IPropertyTypeRepository PropTypes { get; }
        IPropertyRepository Properties { get; }
        IStatusRepository Statuses { get; }
        IProposeRepository Proposes { get; }
        IProviderRepository Providers { get; }
        IReceiptRepository Receipts { get; }
        IDetailedReceiptRepository ReceiptsDetailed { get; }
        int Save();
    }
}

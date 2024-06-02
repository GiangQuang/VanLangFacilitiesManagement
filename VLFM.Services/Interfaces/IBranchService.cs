using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Models;

namespace VLFM.Services.Interfaces
{
    public interface IBranchService
    {
        Task<bool> CreateBranch(BranchDetails branchDetails);
        Task<IEnumerable<BranchDetails>> GetAllBranches();
        Task<BranchDetails> GetBranchById(int Id);
        Task<bool> UpdateBranch(BranchDetails branchDetails);
        Task<bool> DeleteBranch(List<BranchDetails> brs);
    }
}

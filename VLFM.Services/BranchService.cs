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
    public class BranchService : IBranchService
    {
        public IUnitOfWork _unitOfWork;
        public BranchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateBranch(BranchDetails branchDetails)
        {
            if (branchDetails != null)
            {
                await _unitOfWork.Branches.Add(branchDetails);
                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteBranch(List<BranchDetails> brs)
        {
            if (brs != null && brs.Any())
            {
                foreach (var item in brs)
                {

                    var branchDetails = await _unitOfWork.Branches.GetById(item.Id);
                    if (branchDetails != null)
                    {
                        _unitOfWork.Branches.Delete(branchDetails);
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

        public async Task<IEnumerable<BranchDetails>> GetAllBranches()
        {
            var DetailsList = await _unitOfWork.Branches.GetAll();
            return DetailsList;
        }

        public async Task<BranchDetails> GetBranchById(int Id)
        {
            if (Id > 0)
            {
                var branchDetails = await _unitOfWork.Branches.GetById(Id);
                if (branchDetails != null)
                {
                    return branchDetails;
                }
            }
            return null;
        }

        public async Task<bool> UpdateBranch(BranchDetails branchDetails)
        {
            if (branchDetails != null)
            {
                var branch = await _unitOfWork.Branches.GetById(branchDetails.Id);
                if (branch != null)
                {
                    branch.Branchname = branchDetails.Branchname;
                    branch.Address = branchDetails.Address;

                    _unitOfWork.Branches.Update(branch);

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

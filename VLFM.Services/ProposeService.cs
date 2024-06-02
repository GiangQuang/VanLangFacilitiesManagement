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
    public class ProposeService : IProposeService
    {
        public IUnitOfWork _unitOfWork;
        public ProposeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreatePropose(ProposeDetails proposeDetails)
        {
            if (proposeDetails != null)
            {
                await _unitOfWork.Proposes.Add(proposeDetails);
                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeletePropose(List<ProposeDetails> prop)
        {
            if (prop != null && prop.Any())
            {
                foreach (var item in prop)
                {

                    var proposeDetails = await _unitOfWork.Proposes.GetById(item.Id);
                    if (proposeDetails != null)
                    {
                        _unitOfWork.Proposes.Delete(proposeDetails);
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

        public async Task<IEnumerable<ProposeDetails>> GetAllProposes()
        {
            var DetailsList = await _unitOfWork.Proposes.GetAll();
            return DetailsList;
        }

        public async Task<ProposeDetails> GetProposeById(int Id)
        {
            if (Id > 0)
            {
                var proposeDetails = await _unitOfWork.Proposes.GetById(Id);
                if (proposeDetails != null)
                {
                    return proposeDetails;
                }
            }
            return null;
        }

        public async Task<bool> UpdatePropose(ProposeDetails proposeDetails)
        {
            if (proposeDetails != null)
            {
                var propose = await _unitOfWork.Proposes.GetById(proposeDetails.Id);
                if (propose != null)
                {
                    propose.Proposename = proposeDetails.Proposename;
                    propose.Note = proposeDetails.Note;
                    _unitOfWork.Proposes.Update(propose);
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

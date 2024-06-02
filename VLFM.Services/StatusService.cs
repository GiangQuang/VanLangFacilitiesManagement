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
    public class StatusService : IStatusService
    {
        public IUnitOfWork _unitOfWork;
        public StatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateStatus(StatusDetails statusDetails)
        {
            if (statusDetails != null)
            {
                await _unitOfWork.Statuses.Add(statusDetails);
                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteStatus(List<StatusDetails> sta)
        {
            if (sta != null && sta.Any())
            {
                foreach (var item in sta)
                {

                    var statusDetails = await _unitOfWork.Statuses.GetById(item.Id);
                    if (statusDetails != null)
                    {
                        _unitOfWork.Statuses.Delete(statusDetails);
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

        public async Task<IEnumerable<StatusDetails>> GetAllStatuses()
        {
            var DetailsList = await _unitOfWork.Statuses.GetAll();
            return DetailsList;
        }

        public async Task<StatusDetails> GetStatusById(int Id)
        {
            if (Id > 0)
            {
                var statusDetails = await _unitOfWork.Statuses.GetById(Id);
                if (statusDetails != null)
                {
                    return statusDetails;
                }
            }
            return null;
        }

        public async Task<bool> UpdateStatus(StatusDetails statusDetails)
        {
            if (statusDetails != null)
            {
                var status = await _unitOfWork.Statuses.GetById(statusDetails.Id);
                if (status != null)
                {
                    status.Statusname = statusDetails.Statusname;
                    status.Note = statusDetails.Note;
                    _unitOfWork.Statuses.Update(status);
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

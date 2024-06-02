using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Models;

namespace VLFM.Services.Interfaces
{
    public interface IStatusService
    {
        Task<bool> CreateStatus(StatusDetails statusDetails);
        Task<IEnumerable<StatusDetails>> GetAllStatuses();
        Task<StatusDetails> GetStatusById(int Id);
        Task<bool> UpdateStatus(StatusDetails statusDetails);
        Task<bool> DeleteStatus(List<StatusDetails> sta);
    }
}

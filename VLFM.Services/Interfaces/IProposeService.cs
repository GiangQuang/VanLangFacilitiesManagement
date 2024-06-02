using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Models;

namespace VLFM.Services.Interfaces
{
    public interface IProposeService
    {
        Task<bool> CreatePropose(ProposeDetails proposeDetails);
        Task<IEnumerable<ProposeDetails>> GetAllProposes();
        Task<ProposeDetails> GetProposeById(int Id);
        Task<bool> UpdatePropose(ProposeDetails proposeDetails);
        Task<bool> DeletePropose(List<ProposeDetails> prop);
    }
}

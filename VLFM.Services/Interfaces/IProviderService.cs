using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Models;

namespace VLFM.Services.Interfaces
{
    public interface IProviderService
    {
        Task<bool> CreateProvider(ProviderDetails providerDetails);
        Task<IEnumerable<ProviderDetails>> GetAllProviders();
        Task<ProviderDetails> GetProviderById(int Id);
        Task<bool> UpdateProvider(ProviderDetails providerDetails);
        Task<bool> DeleteProvider(List<ProviderDetails> prov);
    }
}

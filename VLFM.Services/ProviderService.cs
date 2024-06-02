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
    public class ProviderService : IProviderService
    {
        public IUnitOfWork _unitOfWork;
        public ProviderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateProvider(ProviderDetails providerDetails)
        {
            if (providerDetails != null)
            {
                await _unitOfWork.Providers.Add(providerDetails);
                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteProvider(List<ProviderDetails> prov)
        {
            if (prov != null && prov.Any())
            {
                foreach (var item in prov)
                {

                    var providerDetails = await _unitOfWork.Providers.GetById(item.Id);
                    if (providerDetails != null)
                    {
                        _unitOfWork.Providers.Delete(providerDetails);
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

        public async Task<IEnumerable<ProviderDetails>> GetAllProviders()
        {
            var DetailsList = await _unitOfWork.Providers.GetAll();
            return DetailsList;
        }

        public async Task<ProviderDetails> GetProviderById(int Id)
        {
            if (Id > 0)
            {
                var providerDetails = await _unitOfWork.Providers.GetById(Id);
                if (providerDetails != null)
                {
                    return providerDetails;
                }
            }
            return null;
        }

        public async Task<bool> UpdateProvider(ProviderDetails providerDetails)
        {
            if (providerDetails != null)
            {
                var provider = await _unitOfWork.Providers.GetById(providerDetails.Id);
                if (provider != null)
                {
                    provider.Providername = providerDetails.Providername;
                    provider.Address = providerDetails.Address;
                    provider.Note = providerDetails.Note;
                    _unitOfWork.Providers.Update(provider);
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

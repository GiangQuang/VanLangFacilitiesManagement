using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Models;
using VLFM.Core.Response;

namespace VLFM.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<bool> CreateProperty(PropertyDetails propertyDetails);
        Task<IEnumerable<PropertyResponse>> GetAllProperties();
        Task<PropertyResponse> GetPropertyById(int Id);
        Task<bool> UpdateProperty(PropertyDetails propertyDetails);
        Task<bool> DeleteProperty(List<PropertyResponse> proper);
    }
}

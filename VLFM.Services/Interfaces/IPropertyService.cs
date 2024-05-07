using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Models;

namespace VLFM.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<bool> CreateProperty(PropertyDetails propertyDetails);
        Task<IEnumerable<PropertyDetails>> GetAllProperties();
        Task<PropertyDetails> GetPropertyById(int Id);
        Task<bool> UpdateProperty(PropertyDetails propertyDetails);
        Task<bool> DeleteProperty(int Id);
    }
}

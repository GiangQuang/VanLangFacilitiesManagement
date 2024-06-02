using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Models;

namespace VLFM.Services.Interfaces
{
    public interface IPropertyTypeService
    {
        Task<bool> CreatePropType(PropertyTypeDetails propertyTypeDetails);
        Task<IEnumerable<PropertyTypeDetails>> GetAllPropTypes();
        Task<PropertyTypeDetails> GetPropTypeById(int Id);
        Task<bool> UpdatePropType(PropertyTypeDetails propertyTypeDetails);
        Task<bool> DeletePropType(List<PropertyTypeDetails> propties);
    }
}

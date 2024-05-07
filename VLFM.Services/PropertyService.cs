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
    public class PropertyService : IPropertyService
    {
        public IUnitOfWork _unitOfWork;
        public PropertyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateProperty(PropertyDetails propertyDetails)
        {
            if (propertyDetails != null)
            {
                propertyDetails.PropertyID = GeneratePropertyID();
                await _unitOfWork.Properties.Add(propertyDetails);
                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteProperty(int Id)
        {
            if (Id > 0)
            {
                var propertyDetails = await _unitOfWork.Properties.GetById(Id);
                if (propertyDetails != null)
                {
                    _unitOfWork.Properties.Delete(propertyDetails);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<PropertyDetails>> GetAllProperties()
        {
            var DetailsList = await _unitOfWork.Properties.GetAll();
            return DetailsList;
        }

        public async Task<PropertyDetails> GetPropertyById(int Id)
        {
            if (Id > 0)
            {
                var propertyDetails = await _unitOfWork.Properties.GetById(Id);
                if (propertyDetails != null)
                {
                    return propertyDetails;
                }
            }
            return null;
        }

        public async Task<bool> UpdateProperty(PropertyDetails propertyDetails)
        {
            if (propertyDetails != null)
            {
                var propertyType = await _unitOfWork.Properties.GetById(propertyDetails.Id);
                if (propertyType != null)
                {
                    propertyType.Propertycode = propertyDetails.Propertycode; 
                    propertyType.Propertyname = propertyDetails.Propertyname;
                    propertyType.Unit = propertyDetails.Unit;
                    propertyType.Note = propertyDetails.Note;
                    _unitOfWork.Properties.Update(propertyType);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        private string GeneratePropertyID()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}

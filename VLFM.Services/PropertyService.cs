using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Interfaces;
using VLFM.Core.Models;
using VLFM.Core.Response;
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
                await _unitOfWork.Properties.Add(propertyDetails);
                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteProperty(List<PropertyResponse> proper)
        {
            if (proper != null && proper.Any())
            {
                foreach (var item in proper)
                {

                    var propertyDetails = await _unitOfWork.Properties.GetById(item.Id);
                    if (propertyDetails != null)
                    {
                        _unitOfWork.Properties.Delete(propertyDetails);
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

        public async Task<IEnumerable<PropertyResponse>> GetAllProperties()
        {
            try
            {
                var properties = await _unitOfWork.Properties.GetAll();
                var propertyTypes = await _unitOfWork.PropTypes.GetAll();

                var query = from prop in properties
                            join propty in propertyTypes on prop.PropTypeID equals propty.PropTypeID
                            select new PropertyResponse
                            {
                                Id = prop.Id,
                                PropertyID = prop.PropertyID,
                                Propertycode = prop.Propertycode,
                                PropTypeID = prop.PropTypeID,
                                Propertyname = prop.Propertyname,
                                Unit = prop.Unit,
                                Note = prop.Note
                            };
                return query.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PropertyResponse> GetPropertyById(int Id)
        {
            if (Id > 0)
            {
                var properties = await _unitOfWork.Properties.GetById(Id);
                var propertyTypes = await _unitOfWork.PropTypes.GetAll();

                if (properties != null)
                {
                    try
                    {
                        var propertyType = propertyTypes.FirstOrDefault(propty => propty.PropTypeID == properties.PropTypeID);
                        var response = new PropertyResponse
                        {
                            Id = properties.Id,
                            PropertyID = properties.PropertyID,
                            Propertycode = properties.Propertycode,
                            PropTypeID = properties.PropTypeID,
                            Propertyname = properties.Propertyname,
                            Unit = properties.Unit,
                            Note = properties.Note
                        };

                        return response;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        public async Task<bool> UpdateProperty(PropertyDetails propertyDetails)
        {
            if (propertyDetails != null)
            {
                var property = await _unitOfWork.Properties.GetById(propertyDetails.Id);
                if (property != null)
                {
                    property.Propertycode = propertyDetails.Propertycode;
                    property.PropTypeID = propertyDetails.PropTypeID;
                    property.Propertyname = propertyDetails.Propertyname;
                    property.Unit = propertyDetails.Unit;
                    property.Note = propertyDetails.Note;
                    _unitOfWork.Properties.Update(property);
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

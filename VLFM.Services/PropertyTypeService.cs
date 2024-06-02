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
    public class PropertyTypeService : IPropertyTypeService
    {
        public IUnitOfWork _unitOfWork;
        public PropertyTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreatePropType(PropertyTypeDetails propertyTypeDetails)
        {
            if (propertyTypeDetails != null)
            {
                await _unitOfWork.PropTypes.Add(propertyTypeDetails);
                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeletePropType(List<PropertyTypeDetails> propties)
        {
            if (propties != null && propties.Any())
            {
                foreach (var item in propties)
                {

                    var propertyTypeDetails = await _unitOfWork.PropTypes.GetById(item.Id);
                    if (propertyTypeDetails != null)
                    {
                        _unitOfWork.PropTypes.Delete(propertyTypeDetails);
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

        public async Task<IEnumerable<PropertyTypeDetails>> GetAllPropTypes()
        {
            var DetailsList = await _unitOfWork.PropTypes.GetAll();
            return DetailsList;
        }

        public async Task<PropertyTypeDetails> GetPropTypeById(int Id)
        {
            if (Id > 0)
            {
                var propertyTypeDetails = await _unitOfWork.PropTypes.GetById(Id);
                if (propertyTypeDetails != null)
                {
                    return propertyTypeDetails;
                }
            }
            return null;
        }

        public async Task<bool> UpdatePropType(PropertyTypeDetails propertyTypeDetails)
        {
            if (propertyTypeDetails != null)
            {
                var propertyType = await _unitOfWork.PropTypes.GetById(propertyTypeDetails.Id);
                if (propertyType != null)
                {
                    propertyType.PropTypename = propertyTypeDetails.PropTypename;
                    propertyType.Note = propertyTypeDetails.Note;

                    _unitOfWork.PropTypes.Update(propertyType);

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

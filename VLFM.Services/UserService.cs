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
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;        
        }

        public async Task<bool> CreateUser(UserDetails userDetails)
        {
            if (userDetails != null)
            {
                await _unitOfWork.Users.Add(userDetails);

                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }
        public async Task<bool> DeleteUser(int Id)
        {
            if (Id > 0)
            {
                var userDetails = await _unitOfWork.Users.GetById(Id);
                if (userDetails != null)
                {
                    _unitOfWork.Users.Delete(userDetails);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        public async Task<IEnumerable<UserDetails>> GetAllUsers()
        {
            var userDetailsList = await _unitOfWork.Users.GetAll();
            return userDetailsList;
        }


        public async Task<UserDetails> GetUserById(int Id)
        {
            if (Id > 0)
            {
                var userDetails = await _unitOfWork.Users.GetById(Id);
                if (userDetails != null)
                {
                    return userDetails;
                }
            }
            return null;
        }
        public async Task<bool> UpdateUser(UserDetails userDetails)
        {
            if (userDetails != null)
            {
                var user = await _unitOfWork.Users.GetById(userDetails.Id);
                if (user != null)
                {
                    user.Fullname = userDetails.Fullname;
                    user.Username = userDetails.Username;
                    user.Password = userDetails.Password;
                    user.Phonenumber = userDetails.Phonenumber;

                    _unitOfWork.Users.Update(user);

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

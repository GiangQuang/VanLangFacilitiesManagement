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
        private readonly IPasswordService _passwordService;
        public UserService(IUnitOfWork unitOfWork, IPasswordService passwordService) 
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;

        }

        public async Task<UserDetails> LoginUser(string Username, string Password)
        {
            var hashedPassword = _passwordService.EncryptPassword(Password);
            var user = await _unitOfWork.Users.GetUserByUsername(Username);
            if (user != null && user.Password == hashedPassword)
            {
                return user; 
            }
            return null;
        }
        

        public async Task<bool> CreateUser(UserDetails userDetails)
        {
            if (userDetails != null)
            {
                userDetails.Password = _passwordService.EncryptPassword(userDetails.Password);
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
                var hashedPassword = _passwordService.EncryptPassword(userDetails.Password);
                var user = await _unitOfWork.Users.GetById(userDetails.Id);
                if (user != null)
                {
                    user.Username = userDetails.Username;
                    user.Password = hashedPassword;
                    user.Status = userDetails.Status;

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

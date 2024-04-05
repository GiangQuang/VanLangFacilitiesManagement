using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Models;

namespace VLFM.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(UserDetails userDetails);

        Task<IEnumerable<UserDetails>> GetAllUsers();

        Task<UserDetails> GetUserById(int Id);

        Task<bool> UpdateUser(UserDetails userDetails);

        Task<bool> DeleteUser(int Id);
    }
}

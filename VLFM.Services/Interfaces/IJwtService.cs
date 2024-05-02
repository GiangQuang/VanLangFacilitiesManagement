using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLFM.Core.Models;

namespace VLFM.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(string Username);
    }

}

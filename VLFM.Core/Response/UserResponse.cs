using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLFM.Core.Response
{
    public class UserResponse
    {
        public int Id { get; set; }
        public object EmployeeID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Status { get; set; }
    }
}

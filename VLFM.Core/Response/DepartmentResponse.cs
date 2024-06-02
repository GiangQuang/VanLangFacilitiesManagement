using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLFM.Core.Response
{
    public class DepartmentResponse
    {
        public int Id { get; set; }
        public string DeptID { get; set; }
        public string BranchID { get; set; }
        public string Deptname { get; set; }
        public string Note { get; set; }
    }
}

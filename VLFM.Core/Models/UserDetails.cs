using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLFM.Core.Models
{
    public class UserDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(14)]
        public string EmployeeID { get; set; }
        [StringLength(15)]
        [RegularExpression("^[a-zA-Z0-9]*$")]
        public string Username { get; set; }
        [StringLength(50)]
        public string Password { get; set; }
        [DefaultValue(0)]
        public int Status { get; set; }
    }
}
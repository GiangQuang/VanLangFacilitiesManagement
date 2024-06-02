using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLFM.Core.Models
{
    public class EmployeeDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDNV { get; set; }
        [Required]
        [StringLength(14)]
        public string EmployeeID { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");
        [StringLength(50)]
        public string Employeename { get; set; }
        [RegularExpression(@"^[0-9;]+$")]
        [StringLength(20)]
        public string Phonenumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime Dateofbirth { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [DefaultValue(0)]
        public int Status { get; set; }
    }
}

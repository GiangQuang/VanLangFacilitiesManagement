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
    public class BranchDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(14)]
        public string BranchID { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");
        [Required]
        [StringLength(50)]
        public string Branchname { get; set; }
        [Required]
        [StringLength(150)]
        public string Address { get; set; }
    }
}

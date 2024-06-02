using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLFM.Core.Models
{
    public class PropertyTypeDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(14)]
        public string PropTypeID { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");
        [Required]
        [StringLength(50)]
        public string PropTypename { get; set; }
        [StringLength(50)]
        public string Note { get; set; }
    }
}

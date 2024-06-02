using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLFM.Core.Models
{
    public class PropertyDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(14)]
        public string PropertyID { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");
        [StringLength(14)]
        public string Propertycode { get; set; }
        [Required]
        [StringLength(14)]
        public string PropTypeID { get; set; }
        [StringLength(150)]
        public string Propertyname { get; set; }
        [StringLength(20)]
        public string Unit { get; set; }
        [StringLength(50)]
        public string Note { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLFM.Core.Models
{
    public class DetailedReceiptDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(14)]
        public string DtReceiptID { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");
        [Required]
        [StringLength(14)]
        public string ReceiptID { get; set; }
        [Required]
        [StringLength(14)]
        public string PropertyID { get; set; }
        [Required]
        public decimal quantity { get; set; }
        public int Price { get; set; }
        [StringLength(150)]
        public string Brand { get; set; }
        [Required]
        public DateTime WarrantydayAt { get; set; }
        [Required]
        public DateTime WarrantydayEnd { get; set; }
        [Required]
        [StringLength(14)]
        public string StatusID { get; set; }
        [Required]
        [StringLength(14)]
        public string ProposeID { get; set; }
    }
}

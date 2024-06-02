using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLFM.Core.Models
{
    public class ReceiptDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(14)]
        public string ReceiptID { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");
        public DateTime Date { get; set; }
        [Required]
        [StringLength(14)]
        public string EmployeeID { get; set; }
        [Required]
        [StringLength(14)]
        public string ProviderID { get; set; }
        [Required]
        [StringLength(10)]
        public string Receiptcode { get; set; }
        [StringLength(50)]
        public string Note { get; set; }
    }
}

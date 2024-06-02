using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLFM.Core.Response
{
    public class ReceiptResponse
    {
        public int Id { get; set; }
        public string ReceiptID { get; set; }
        public DateTime Date { get; set; }
        public string EmployeeID { get; set; }
        public string ProviderID { get; set; }
        public string Receiptcode { get; set; }
        public string Note { get; set; }
    }
}

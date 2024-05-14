using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLFM.Core.Response
{
    public class DetailedReceiptResponse
    {
        public int Id { get; set; }
        public string DtReceiptID { get; set; }
        public object ReceiptID { get; set; }
        public object PropertyID { get; set; }
        public decimal quantity { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }
        public DateTime WarrantydayAt { get; set; }
        public DateTime WarrantydayEnd { get; set; }
        public object StatusID { get; set; }
        public object ProposeID { get; set; }
    }
}

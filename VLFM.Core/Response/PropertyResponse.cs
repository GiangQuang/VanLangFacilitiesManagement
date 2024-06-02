using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLFM.Core.Response
{
    public class PropertyResponse
    {
        public int Id { get; set; }
        public string PropertyID { get; set; }
        public string Propertycode { get; set; }
        public string PropTypeID { get; set; }
        public string Propertyname { get; set; }
        public string Unit { get; set; }
        public string Note { get; set; }
    }
}

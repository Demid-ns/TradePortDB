using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TPDB.Resource.API.Models
{
    public class CommodityPostRequest
    {
        public int PortId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool ForImport { get; set; }
    }
}

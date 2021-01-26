using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TPDB.Resource.API.Models
{
    public class Commodity
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool ForImport { get; set; }
    }
}

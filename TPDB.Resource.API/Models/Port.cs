using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TPDB.Resource.API.Models
{
    public class Port
    {
        public int Id { get; set; }
        public string System { get; set; }
        public string Body { get; set; }
        public List<Commodity> Commodities { get; set; }

        public Port()
        {
            Commodities = new List<Commodity> { };
        }
    }
}

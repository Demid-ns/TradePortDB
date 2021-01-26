using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TPDB.Resource.API.Models
{
    public class PortPostRequest
    {
        [Required]
        public string System { get; set; }
        [Required]
        public string Body { get; set; }
    }
}

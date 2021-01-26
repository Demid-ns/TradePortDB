using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TPDB.Resource.API.Models
{
    public class ProductPostRequest
    {
        [Required]
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TPDB.Auth.API.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //Многие ко многим
        public ICollection<Role> Roles { get; set; }

        public Account()
        {
            Roles = new List<Role> { };
        }
    }
}

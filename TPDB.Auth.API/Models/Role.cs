using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TPDB.Auth.API.Models
{
    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }
        //Многие ко многим
        public ICollection<Account> Accounts { get; set; }

        public Role()
        {
            Accounts = new List<Account> { };
        }
    }
}

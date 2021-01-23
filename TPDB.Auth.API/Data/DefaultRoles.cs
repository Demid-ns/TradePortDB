using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPDB.Auth.API.Models;

namespace TPDB.Auth.API.Data
{
    public class DefaultRole
    {
        public static void AddDefaultData(TPDBContext context)
        {
            if (!context.Roles.Any())
            {
                context.AddRange(
                    new Role
                    {
                        Name = "admin"
                    }, new Role
                    {
                        Name = "user"
                    }
                    );
            }
            context.SaveChanges();
        }
    }
}

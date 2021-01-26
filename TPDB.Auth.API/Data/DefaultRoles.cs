using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPDB.Auth.API.Models;

namespace TPDB.Auth.API.Data
{
    public class DefaultRole
    {
        //Роли для инициализации БД
        public static void AddDefaultData(TPDBAuthContext context)
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

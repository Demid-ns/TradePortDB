using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPDB.Auth.API.Models;

namespace TPDB.Auth.API.Data
{
    public class DefaultAccounts
    {
        //Аккаунты для инициализации БД
        public static void AddDefaultData(TPDBAuthContext context)
        {
            if (!context.Users.Any())
            {
                Role adminRole = context.Roles.SingleOrDefault(r => r.Name == "admin");
                Role userRole = context.Roles.SingleOrDefault(r => r.Name == "user");

                Account account1 = new Account()
                {
                    Id = Guid.Parse("e5e3ab70-5b98-40e6-9194-d78b1a24421c"),
                    Email = "user@email.com",
                    Password = "user"
                };
                account1.Roles.Add(adminRole);

                Account account2 = new Account()
                {
                    Id = Guid.Parse("65a10b87-0405-44eb-8882-0889480bbf93"),
                    Email = "user2@email.com",
                    Password = "user2"
                };
                account2.Roles.Add(userRole);

                Account account3 = new Account()
                {
                    Id = Guid.Parse("76edca38-46ae-4efe-ae06-c122b091a06c"),
                    Email = "user3@email.com",
                    Password = "user3"
                };
                account3.Roles.Add(userRole);

                context.AddRange(account1, account2, account3);
            }
            context.SaveChanges();
        }
    }
}

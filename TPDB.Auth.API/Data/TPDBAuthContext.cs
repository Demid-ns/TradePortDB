using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPDB.Auth.API.Models;

namespace TPDB.Auth.API.Data
{
    public class TPDBAuthContext: DbContext
    {
        public DbSet<Account> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public TPDBAuthContext(DbContextOptions<TPDBAuthContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

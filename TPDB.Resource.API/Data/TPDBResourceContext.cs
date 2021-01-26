using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPDB.Resource.API.Models;

namespace TPDB.Resource.API.Data
{
    public class TPDBResourceContext : DbContext
    {
        public DbSet<Port> Ports { get; set; }
        public DbSet<Commodity> Commodities { get; set; }
        public DbSet<Product> Products { get; set; }

        public TPDBResourceContext(DbContextOptions<TPDBResourceContext> options)
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

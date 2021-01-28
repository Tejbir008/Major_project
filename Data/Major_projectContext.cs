using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Major_project.Models;

namespace Major_project.Data
{
    public class Major_projectContext : DbContext
    {
        public Major_projectContext (DbContextOptions<Major_projectContext> options)
            : base(options)
        {
        }

        public DbSet<Major_project.Models.Customer> Customer { get; set; }

        public DbSet<Major_project.Models.Food> Food { get; set; }

        public DbSet<Major_project.Models.Order> Order { get; set; }

        public DbSet<Major_project.Models.Staff> Staff { get; set; }

        public DbSet<Major_project.Models.Store> Store { get; set; }
    }
}

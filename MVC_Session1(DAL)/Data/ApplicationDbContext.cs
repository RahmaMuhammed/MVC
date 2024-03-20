using Microsoft.EntityFrameworkCore;
using MVC_Session1_DAL_.Data.Configurations;
using MVC_Session1_DAL_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Session1_DAL_.Data
{
    internal class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server = .; DataBase = MvcApplicationSession1; Trusted_Connection = True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //  modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Department>Departments { get; set; }
    }
}

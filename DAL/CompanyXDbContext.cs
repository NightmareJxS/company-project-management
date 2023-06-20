using DAL.DataSeeding;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CompanyXDbContext : DbContext
    {
        public CompanyXDbContext()
        {
            
        }

        public CompanyXDbContext(DbContextOptions<CompanyXDbContext> options) : base(options) { }

        public DbSet<CompanyProject> CompanyProjects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ParticipatingProject> ParticipatingProjects { get; set; }
        public DbSet<HRStaff> HRStaffs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration config = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json").Build();


                string connectionString = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParticipatingProject>()
                .HasKey(pp => new { pp.CompanyProjectID, pp.EmployeeID });

            //modelBuilder.Entity<CompanyProject>()
            //    .HasMany(cp => cp.ParticipatingProjects)
            //    .WithOne(pp => pp.CompanyProject)
            //    .HasForeignKey(pp => pp.CompanyProjectID);

            //modelBuilder.Entity<Employee>()
            //    .HasMany(e => e.ParticipatingProjects)
            //    .WithOne(pp => pp.Employee)
            //    .HasForeignKey(pp => pp.EmployeeID);

            //modelBuilder.Entity<Department>()
            //    .HasMany(d => d.Employees)
            //    .WithOne(e => e.Department)
            //    .HasForeignKey(e => e.DepartmentID);


            #region Seed Data
            modelBuilder.SeedHrStaff();
            modelBuilder.SeedDepartment();
            modelBuilder.SeedEmployee();
            #endregion
        }
    }
}

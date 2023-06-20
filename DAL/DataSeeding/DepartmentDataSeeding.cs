using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataSeeding
{
    public static class DepartmentDataSeeding
    {
        public static void SeedDepartment(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasData(
                    new Department()
                    {
                        DepartmentID = 1,
                        DepartmentName = "IT",
                        DepartmentDescription = "Information Technology Department"
                    },
                    new Department()
                    {
                        DepartmentID = 2,
                        DepartmentName = "Marketing",
                        DepartmentDescription = "Marketing Department"
                    },
                    new Department()
                    {
                        DepartmentID = 3,
                        DepartmentName = "Sales",
                        DepartmentDescription = "Sales Department"
                    },
                    new Department()
                    {
                        DepartmentID = 4,
                        DepartmentName = "Finance",
                        DepartmentDescription = "Finance Department"
                    },
                    new Department()
                    {
                        DepartmentID = 5,
                        DepartmentName = "HR",
                        DepartmentDescription = "Human Resources Department"
                    }
                );
        }
    }
}

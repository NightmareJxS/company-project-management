using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataSeeding
{
    public static class EmployeeDataSeeding
    {
        public static void SeedEmployee(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasData(
                    new Employee()
                    {
                        EmployeeID = 1,
                        FullName = "John Smith",
                        Skills = "Java, Python, SQL",
                        Telephone = "1234567890",
                        Address = "123 Main St, City",
                        Status = EmployeeStatus.Active,
                        DepartmentID = 1
                    },
                    new Employee()
                    {
                        EmployeeID = 2,
                        FullName = "Emily Johnson",
                        Skills = "C#, JavaScript, HTML",
                        Telephone = "9876543210",
                        Address = "456 Elm St, City",
                        Status = EmployeeStatus.InActive,
                        DepartmentID = 1
                    },
                    new Employee()
                    {
                        EmployeeID = 3,
                        FullName = "Michael Brown",
                        Skills = "PHP, MySQL, Laravel",
                        Telephone = "5555555555",
                        Address = "789 Oak St, City",
                        Status = EmployeeStatus.Active,
                        DepartmentID = 1
                    },
                    new Employee()
                    {
                        EmployeeID = 4,
                        FullName = "Jessica Davis",
                        Skills = "Marketing Strategy, Social Media",
                        Telephone = "1111111111",
                        Address = "321 Maple St, City",
                        Status = EmployeeStatus.Active,
                        DepartmentID = 2
                    },
                    new Employee()
                    {
                        EmployeeID = 5,
                        FullName = "David Wilson",
                        Skills = "Market Research, Advertising",
                        Telephone = "9999999999",
                        Address = "654 Pine St, City",
                        Status = EmployeeStatus.Active,
                        DepartmentID = 2
                    },
                    new Employee()
                    {
                        EmployeeID = 6,
                        FullName = "Jennifer Thompson",
                        Skills = "Salesforce, Customer Relationship Management",
                        Telephone = "7777777777",
                        Address = "987 Cedar St, City",
                        Status = EmployeeStatus.Active,
                        DepartmentID = 3
                    },
                    new Employee()
                    {
                        EmployeeID = 7,
                        FullName = "Christopher Martinez",
                        Skills = "Negotiation, Sales Strategies",
                        Telephone = "2222222222",
                        Address = "111 Walnut St, City",
                        Status = EmployeeStatus.Active,
                        DepartmentID = 3
                    },
                    new Employee()
                    {
                        EmployeeID = 8,
                        FullName = "Elizabeth Turner",
                        Skills = "Accounting, Financial Analysis",
                        Telephone = "3333333333",
                        Address = "555 Cherry St, City",
                        Status = EmployeeStatus.Active,
                        DepartmentID = 4
                    },
                    new Employee()
                    {
                        EmployeeID = 9,
                        FullName = "Matthew Harris",
                        Skills = "Budgeting, Tax Planning",
                        Telephone = "4444444444",
                        Address = "777 Olive St, City",
                        Status = EmployeeStatus.InActive,
                        DepartmentID = 4
                    },
                    new Employee()
                    {
                        EmployeeID = 10,
                        FullName = "Sophia Clark",
                        Skills = "Business Consulting, Project Management",
                        Telephone = "6666666666",
                        Address = "333 Pine St, City",
                        Status = EmployeeStatus.Active,
                        DepartmentID = 5
                    }
                );
        }

    }
}

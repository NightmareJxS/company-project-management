using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentID", "DepartmentDescription", "DepartmentName" },
                values: new object[,]
                {
                    { 1, "Information Technology Department", "IT" },
                    { 2, "Marketing Department", "Marketing" },
                    { 3, "Sales Department", "Sales" },
                    { 4, "Finance Department", "Finance" },
                    { 5, "Human Resources Department", "HR" }
                });

            migrationBuilder.InsertData(
                table: "HRStaffs",
                columns: new[] { "Email", "BirthDate", "FullName", "Password", "PropjectRole" },
                values: new object[,]
                {
                    { "a@a.a", new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Testing Admin", "123", 0 },
                    { "b@b.b", new DateTime(1999, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Testing Admin 2", "456", 0 },
                    { "c@c.c", new DateTime(1999, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Testing ProjectManager 1", "123", 1 },
                    { "d@d.d", new DateTime(1999, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Testing ProjectManager 2", "456", 1 },
                    { "e@e.e", new DateTime(1999, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Testing HRStaff 1", "123", 2 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "Address", "DepartmentID", "FullName", "Skills", "Status", "Telephone" },
                values: new object[,]
                {
                    { 1, "Address 1", 1, "Employee 1", "Programming", 0, "0123456789" },
                    { 2, "Address 2", 1, "Employee 2", "Programming", 1, "0123456789" },
                    { 3, "Address 3", 1, "Employee 3", "Programming", 0, "0123456789" },
                    { 4, "Address 4", 2, "Employee 4", "Marketing", 0, "0123456789" },
                    { 5, "Address 5", 2, "Employee 5", "Marketing", 0, "0123456789" },
                    { 6, "Address 6", 3, "Employee 6", "Sales", 0, "0123456789" },
                    { 7, "Address 7", 3, "Employee 7", "Sales", 0, "0123456789" },
                    { 8, "Address 8", 4, "Employee 8", "Finance", 0, "0123456789" },
                    { 9, "Address 9", 4, "Employee 9", "Finance", 1, "0123456789" },
                    { 10, "Address 10", 5, "Employee 10", "Consulting", 0, "0123456789" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "EmployeeID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "HRStaffs",
                keyColumn: "Email",
                keyValue: "a@a.a");

            migrationBuilder.DeleteData(
                table: "HRStaffs",
                keyColumn: "Email",
                keyValue: "b@b.b");

            migrationBuilder.DeleteData(
                table: "HRStaffs",
                keyColumn: "Email",
                keyValue: "c@c.c");

            migrationBuilder.DeleteData(
                table: "HRStaffs",
                keyColumn: "Email",
                keyValue: "d@d.d");

            migrationBuilder.DeleteData(
                table: "HRStaffs",
                keyColumn: "Email",
                keyValue: "e@e.e");

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "DepartmentID",
                keyValue: 5);
        }
    }
}

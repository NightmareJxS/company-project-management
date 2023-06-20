using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyProjects",
                columns: table => new
                {
                    CompanyProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimatedStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyProjects", x => x.CompanyProjectID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "HRStaffs",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PropjectRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRStaffs", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParticipatingProjects",
                columns: table => new
                {
                    CompanyProjectID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipatingProjects", x => new { x.CompanyProjectID, x.EmployeeID });
                    table.ForeignKey(
                        name: "FK_ParticipatingProjects_CompanyProjects_CompanyProjectID",
                        column: x => x.CompanyProjectID,
                        principalTable: "CompanyProjects",
                        principalColumn: "CompanyProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipatingProjects_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    { "admin@example.com", new DateTime(1985, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Doe", "admin@123", 0 },
                    { "hr1@example.com", new DateTime(1993, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "David Johnson", "hr@789", 2 },
                    { "hr2@example.com", new DateTime(1992, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sarah Williams", "hr@987", 2 },
                    { "pm1@example.com", new DateTime(1990, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Smith", "pm@456", 1 },
                    { "pm2@example.com", new DateTime(1988, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Michael Johnson", "pm@789", 1 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "Address", "DepartmentID", "FullName", "Skills", "Status", "Telephone" },
                values: new object[,]
                {
                    { 1, "123 Main St, City", 1, "John Smith", "Java, Python, SQL", 0, "1234567890" },
                    { 2, "456 Elm St, City", 1, "Emily Johnson", "C#, JavaScript, HTML", 1, "9876543210" },
                    { 3, "789 Oak St, City", 1, "Michael Brown", "PHP, MySQL, Laravel", 0, "5555555555" },
                    { 4, "321 Maple St, City", 2, "Jessica Davis", "Marketing Strategy, Social Media", 0, "1111111111" },
                    { 5, "654 Pine St, City", 2, "David Wilson", "Market Research, Advertising", 0, "9999999999" },
                    { 6, "987 Cedar St, City", 3, "Jennifer Thompson", "Salesforce, Customer Relationship Management", 0, "7777777777" },
                    { 7, "111 Walnut St, City", 3, "Christopher Martinez", "Negotiation, Sales Strategies", 0, "2222222222" },
                    { 8, "555 Cherry St, City", 4, "Elizabeth Turner", "Accounting, Financial Analysis", 0, "3333333333" },
                    { 9, "777 Olive St, City", 4, "Matthew Harris", "Budgeting, Tax Planning", 1, "4444444444" },
                    { 10, "333 Pine St, City", 5, "Sophia Clark", "Business Consulting, Project Management", 0, "6666666666" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentID",
                table: "Employees",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipatingProjects_EmployeeID",
                table: "ParticipatingProjects",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HRStaffs");

            migrationBuilder.DropTable(
                name: "ParticipatingProjects");

            migrationBuilder.DropTable(
                name: "CompanyProjects");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}

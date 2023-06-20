﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(CompanyXDbContext))]
    [Migration("20230609182307_SeedData")]
    partial class SeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DAL.Entities.CompanyProject", b =>
                {
                    b.Property<int>("CompanyProjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyProjectID"));

                    b.Property<DateTime>("EstimatedEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EstimatedStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProjectDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyProjectID");

                    b.ToTable("CompanyProjects");
                });

            modelBuilder.Entity("DAL.Entities.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentID"));

                    b.Property<string>("DepartmentDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentID");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            DepartmentID = 1,
                            DepartmentDescription = "Information Technology Department",
                            DepartmentName = "IT"
                        },
                        new
                        {
                            DepartmentID = 2,
                            DepartmentDescription = "Marketing Department",
                            DepartmentName = "Marketing"
                        },
                        new
                        {
                            DepartmentID = 3,
                            DepartmentDescription = "Sales Department",
                            DepartmentName = "Sales"
                        },
                        new
                        {
                            DepartmentID = 4,
                            DepartmentDescription = "Finance Department",
                            DepartmentName = "Finance"
                        },
                        new
                        {
                            DepartmentID = 5,
                            DepartmentDescription = "Human Resources Department",
                            DepartmentName = "HR"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skills")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmployeeID = 1,
                            Address = "Address 1",
                            DepartmentID = 1,
                            FullName = "Employee 1",
                            Skills = "Programming",
                            Status = 0,
                            Telephone = "0123456789"
                        },
                        new
                        {
                            EmployeeID = 2,
                            Address = "Address 2",
                            DepartmentID = 1,
                            FullName = "Employee 2",
                            Skills = "Programming",
                            Status = 1,
                            Telephone = "0123456789"
                        },
                        new
                        {
                            EmployeeID = 3,
                            Address = "Address 3",
                            DepartmentID = 1,
                            FullName = "Employee 3",
                            Skills = "Programming",
                            Status = 0,
                            Telephone = "0123456789"
                        },
                        new
                        {
                            EmployeeID = 4,
                            Address = "Address 4",
                            DepartmentID = 2,
                            FullName = "Employee 4",
                            Skills = "Marketing",
                            Status = 0,
                            Telephone = "0123456789"
                        },
                        new
                        {
                            EmployeeID = 5,
                            Address = "Address 5",
                            DepartmentID = 2,
                            FullName = "Employee 5",
                            Skills = "Marketing",
                            Status = 0,
                            Telephone = "0123456789"
                        },
                        new
                        {
                            EmployeeID = 6,
                            Address = "Address 6",
                            DepartmentID = 3,
                            FullName = "Employee 6",
                            Skills = "Sales",
                            Status = 0,
                            Telephone = "0123456789"
                        },
                        new
                        {
                            EmployeeID = 7,
                            Address = "Address 7",
                            DepartmentID = 3,
                            FullName = "Employee 7",
                            Skills = "Sales",
                            Status = 0,
                            Telephone = "0123456789"
                        },
                        new
                        {
                            EmployeeID = 8,
                            Address = "Address 8",
                            DepartmentID = 4,
                            FullName = "Employee 8",
                            Skills = "Finance",
                            Status = 0,
                            Telephone = "0123456789"
                        },
                        new
                        {
                            EmployeeID = 9,
                            Address = "Address 9",
                            DepartmentID = 4,
                            FullName = "Employee 9",
                            Skills = "Finance",
                            Status = 1,
                            Telephone = "0123456789"
                        },
                        new
                        {
                            EmployeeID = 10,
                            Address = "Address 10",
                            DepartmentID = 5,
                            FullName = "Employee 10",
                            Skills = "Consulting",
                            Status = 0,
                            Telephone = "0123456789"
                        });
                });

            modelBuilder.Entity("DAL.Entities.HRStaff", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropjectRole")
                        .HasColumnType("int");

                    b.HasKey("Email");

                    b.ToTable("HRStaffs");

                    b.HasData(
                        new
                        {
                            Email = "a@a.a",
                            BirthDate = new DateTime(1999, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FullName = "Testing Admin",
                            Password = "123",
                            PropjectRole = 0
                        },
                        new
                        {
                            Email = "b@b.b",
                            BirthDate = new DateTime(1999, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FullName = "Testing Admin 2",
                            Password = "456",
                            PropjectRole = 0
                        },
                        new
                        {
                            Email = "c@c.c",
                            BirthDate = new DateTime(1999, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FullName = "Testing ProjectManager 1",
                            Password = "123",
                            PropjectRole = 1
                        },
                        new
                        {
                            Email = "d@d.d",
                            BirthDate = new DateTime(1999, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FullName = "Testing ProjectManager 2",
                            Password = "456",
                            PropjectRole = 1
                        },
                        new
                        {
                            Email = "e@e.e",
                            BirthDate = new DateTime(1999, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FullName = "Testing HRStaff 1",
                            Password = "123",
                            PropjectRole = 2
                        });
                });

            modelBuilder.Entity("DAL.Entities.ParticipatingProject", b =>
                {
                    b.Property<int>("CompanyProjectID")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CompanyProjectID", "EmployeeID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("ParticipatingProjects");
                });

            modelBuilder.Entity("DAL.Entities.Employee", b =>
                {
                    b.HasOne("DAL.Entities.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("DAL.Entities.ParticipatingProject", b =>
                {
                    b.HasOne("DAL.Entities.CompanyProject", "CompanyProject")
                        .WithMany("ParticipatingProjects")
                        .HasForeignKey("CompanyProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.Employee", "Employee")
                        .WithMany("ParticipatingProjects")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CompanyProject");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("DAL.Entities.CompanyProject", b =>
                {
                    b.Navigation("ParticipatingProjects");
                });

            modelBuilder.Entity("DAL.Entities.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("DAL.Entities.Employee", b =>
                {
                    b.Navigation("ParticipatingProjects");
                });
#pragma warning restore 612, 618
        }
    }
}

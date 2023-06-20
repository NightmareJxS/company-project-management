using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataSeeding
{
    public static class HrStaffDataSeeding
    {
        public static void SeedHrStaff(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HRStaff>()
                .HasData(
                    // Administator
                    new HRStaff()
                    {
                        Email = "admin@example.com",
                        FullName = "John Doe",
                        Password = "admin@123",
                        BirthDate = new DateTime(1985, 5, 10),
                        PropjectRole = PropjectRole.Administator
                    },
                    // Project Managers
                    new HRStaff()
                    {
                        Email = "pm1@example.com",
                        FullName = "Jane Smith",
                        Password = "pm@456",
                        BirthDate = new DateTime(1990, 9, 20),
                        PropjectRole = PropjectRole.ProjectManager
                    },
                    new HRStaff()
                    {
                        Email = "pm2@example.com",
                        FullName = "Michael Johnson",
                        Password = "pm@789",
                        BirthDate = new DateTime(1988, 3, 25),
                        PropjectRole = PropjectRole.ProjectManager
                    },
                    // HR Staff
                    new HRStaff()
                    {
                        Email = "hr1@example.com",
                        FullName = "David Johnson",
                        Password = "hr@789",
                        BirthDate = new DateTime(1993, 7, 15),
                        PropjectRole = PropjectRole.HRStaff
                    },
                    new HRStaff()
                    {
                        Email = "hr2@example.com",
                        FullName = "Sarah Williams",
                        Password = "hr@987",
                        BirthDate = new DateTime(1992, 11, 5),
                        PropjectRole = PropjectRole.HRStaff
                    }
                );
        }

    }
}

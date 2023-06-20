using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Skills { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public EmployeeStatus Status { get; set; }
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<ParticipatingProject> ParticipatingProjects { get; set; }
    }

    public enum EmployeeStatus
    {
        Active,
        InActive
    }
}

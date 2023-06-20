using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class HRStaff
    {
        [Key]
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public PropjectRole PropjectRole { get; set; }
    }

    public enum PropjectRole
    {
        Administator,
        ProjectManager,
        HRStaff
    }
}

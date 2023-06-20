using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ParticipatingProject
    {
        [Key]
        public int CompanyProjectID { get; set; }
        public virtual CompanyProject CompanyProject { get; set; }
        [Key]
        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

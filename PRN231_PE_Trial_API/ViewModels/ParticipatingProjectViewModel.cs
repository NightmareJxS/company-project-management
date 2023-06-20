using DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace PRN231_PE_Trial_API.ViewModels
{
    public class ParticipatingProjectViewModel
    {
        public int? CompanyProjectID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EmployeeViewModel? Employee { get; set; }
    }
}

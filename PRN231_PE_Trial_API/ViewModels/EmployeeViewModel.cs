using DAL.Entities;

namespace PRN231_PE_Trial_API.ViewModels
{
    public class EmployeeViewModel
    {
        public int? EmployeeID { get; set; }
        public string? FullName { get; set; }
        public string? Skills { get; set; }
        public string? Telephone { get; set; }
        public string? Address { get; set; }
        public EmployeeStatus? Status { get; set; }
        public int? DepartmentID { get; set; }
        public DepartmentViewModel? Department { get; set; }
        public ICollection<ParticipatingProjectViewModel> ParticipatingProjects { get; set; }
    }
}

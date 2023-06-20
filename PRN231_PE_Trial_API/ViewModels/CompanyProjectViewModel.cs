using DAL.Entities;

namespace PRN231_PE_Trial_API.ViewModels
{
    public class CompanyProjectViewModel
    {
        public int? CompanyProjectID { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public DateTime? EstimatedStartDate { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public IList<ParticipatingProjectViewModel> ParticipatingProjects { get; set; }
    }
}

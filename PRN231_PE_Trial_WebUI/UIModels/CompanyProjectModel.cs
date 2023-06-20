namespace PRN231_PE_Trial_WebUI.UIModels
{
    public class CompanyProjectModel
    {
        public int? CompanyProjectID { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectDescription { get; set; }
        public DateTime? EstimatedStartDate { get; set; }
        public DateTime? EstimatedEndDate { get; set; }
        public IList<ParticipatingProjectModel>? ParticipatingProjects { get; set; } = new List<ParticipatingProjectModel>();
    }
}

namespace PRN231_PE_Trial_WebUI.UIModels
{
    public class ParticipatingProjectModel
    {
        public int? CompanyProjectID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EmployeeModel? Employee { get; set; }
    }
}
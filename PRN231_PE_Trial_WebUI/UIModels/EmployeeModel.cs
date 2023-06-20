namespace PRN231_PE_Trial_WebUI.UIModels
{
    public class EmployeeModel
    {
        public int? EmployeeID { get; set; }
        public string? FullName { get; set; }
        public string? Skills { get; set; }
        public string? Telephone { get; set; }
        public string? Address { get; set; }
        public EmployeeStatus Status { get; set; }
        public int? DepartmentID { get; set; }
        public DepartmentModel? Department { get; set; }

        public enum EmployeeStatus
        {
            Active,
            InActive
        }
    }
}
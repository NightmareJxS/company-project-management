using DAL.Entities;

namespace PRN231_PE_Trial_API.ViewModels
{
    public class HrStaffViewModel
    {
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public PropjectRole? PropjectRole { get; set; }
    }
}

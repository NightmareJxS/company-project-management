using System.ComponentModel.DataAnnotations;

namespace PRN231_PE_Trial_WebUI.UIModels
{
    public class LoginModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Password { get; set; }
    }
}

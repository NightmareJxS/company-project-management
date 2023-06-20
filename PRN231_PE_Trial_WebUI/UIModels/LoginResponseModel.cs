namespace PRN231_PE_Trial_WebUI.UIModels
{
    public class LoginResponseModel
    {
        public bool Status { get; set; }
        public string ErrorMessage { get; set; }
        public TokenResponseModel? Token { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN231_PE_Trial_WebUI.UIModels;
using System.Net.Http;
using System.Text;

namespace PRN231_PE_Trial_WebUI.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginPageModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public LoginModel loginModel{ get; set; }

        public async Task<IActionResult> OnPostLogin()
        {
            #region Call Auth API
            var apiUrl = "https://localhost:7202/api/Authenticate";

            try
            {
                var response = await _httpClientFactory.CreateClient().PostAsJsonAsync(apiUrl, loginModel);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(responseContent);
                    if (loginResponse.Status)
                    {
                        // Authentication succeeded
                        var jwtToken = loginResponse.Token.JwtToken;
                        var refreshToken = loginResponse.Token.RefreshToken;

                        SetTokenCookie(jwtToken, refreshToken);

                        // Authentication succeeded so redirect to the Management page
                        return RedirectToPage("/CompanyProjectPages/Management");
                    }
                    else
                    {
                        // Authentication failed so display the error message
                        ModelState.AddModelError("API_Error", loginResponse.ErrorMessage);
                    }
                }
                else
                {
                    // Invalid response structure
                    ModelState.AddModelError("API_Error", "Invalid response from the API");
                }

            }
            catch (Exception ex)
            {
                // An exception occurred while calling the API
                // Handle the exception as needed
                throw;
            }
            #endregion

            // Authentication failed
            return Page();
        }

        private void SetTokenCookie(string jwtToken, string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                Path = "/",
                Secure = true,
                HttpOnly = true,
                Expires = DateTime.Now.AddMinutes(30)
            };

            Response.Cookies.Append("jwtToken", jwtToken, cookieOptions);
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}

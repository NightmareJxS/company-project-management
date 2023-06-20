using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PRN231_PE_Trial_WebUI.UIModels;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;

namespace PRN231_PE_Trial_WebUI.Pages.CompanyProjectPages
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public CompanyProjectModel CompanyProject { get; set; }

        public async Task OnGet(int id)
        {
            #region Get tokens from browser cookies
            string jwtToken = Request.Cookies["jwtToken"];
            string refreshToken = Request.Cookies["refreshToken"];

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = new JwtSecurityToken();

            if (jwtToken.IsNullOrEmpty())
            {
                ModelState.AddModelError("Token_Error", "Cookie contain JWT Token was not found!");
                token = null;
            }
            else
            {
                token = tokenHandler.ReadJwtToken(jwtToken);
            }

            //JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            #endregion

            #region Validate JWT Token
            // Validate the cookie contain the JWT token
            if (token == null)
            {
                ModelState.AddModelError("Token_Error", "Browser Cookie containing JWT Token Expired!");
            }
            else
            {
                // Validate jwtToken ExpiredDate
                long expriedUnixDate = Convert.ToInt64(token.Claims.FirstOrDefault(c => c.Type == "exp")?.Value);
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(expriedUnixDate);
                DateTime tokenExpiredDate = dateTimeOffset.LocalDateTime;

                if (tokenExpiredDate < DateTime.Now)
                {
                    ModelState.AddModelError("Token_Error", "jwtToken Expired!");
                }

                // Validate jwtToken Role
                string? userRole = token.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
                if (userRole != null)
                {
                    if (!userRole.Equals("ProjectManager"))
                    {
                        ModelState.AddModelError("Token_Error", "You are not allowed to access this function!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Token_Error", "There is no ClaimType.Role in this jwt token!");
                }
            }
            #endregion

            #region Call Odata API to set up CompanyProject
            // Only run if there's no problem with jwt Token
            if (ModelState.ErrorCount == 0)
            {
                // Only get CompanyProject
                var apiUrl = $"https://localhost:7202/odata/CompanyProject?$expand=participatingProjects($expand=employee($expand=department))&$filter=CompanyProjectID eq {id}";
                // Note: $count from OData count all the record INSIDE DB, not the records that had QUERIED up
                // Note 2: Exception with $filter query, only count number of records AFTER Filter

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(apiUrl),
                    Method = HttpMethod.Get,
                };

                //add jwt to authorization header
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                try
                {
                    //var response = await _httpClientFactory.CreateClient().GetAsync(apiUrl);
                    var client = _httpClientFactory.CreateClient();
                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var odataObject = JsonConvert.DeserializeObject<OdataCompanyProjectModel>(responseContent);

                        if (odataObject != null)
                        {
                            if (odataObject.Value.Count != 0)
                            {
                                CompanyProject = odataObject.Value[0];// Get the Filtered Value
                            }
                            else
                            {
                                ModelState.AddModelError("Odata_Error", "Failed to get Company Project");
                            }

                        }
                        else
                        {
                            ModelState.AddModelError("Odata_Error", "Failed JsonConvert");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Odata_Error", "Invalid response from the API");
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Odata_Error", ex.Message);
                }
            }
            #endregion
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            #region Get tokens from browser cookies
            string jwtToken = Request.Cookies["jwtToken"];
            string refreshToken = Request.Cookies["refreshToken"];

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = new JwtSecurityToken();

            if (jwtToken.IsNullOrEmpty())
            {
                ModelState.AddModelError("Token_Error", "Cookie contain JWT Token was not found!");
                token = null;
            }
            else
            {
                token = tokenHandler.ReadJwtToken(jwtToken);
            }

            //JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            #endregion

            #region Validate JWT Token
            // Validate the cookie contain the JWT token
            if (token == null)
            {
                ModelState.AddModelError("Token_Error", "Browser Cookie containing JWT Token Expired!");
            }
            else
            {
                // Validate jwtToken ExpiredDate
                long expriedUnixDate = Convert.ToInt64(token.Claims.FirstOrDefault(c => c.Type == "exp")?.Value);
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(expriedUnixDate);
                DateTime tokenExpiredDate = dateTimeOffset.LocalDateTime;

                if (tokenExpiredDate < DateTime.Now)
                {
                    ModelState.AddModelError("Token_Error", "jwtToken Expired!");
                }

                // Validate jwtToken Role
                string? userRole = token.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
                if (userRole != null)
                {
                    if (!userRole.Equals("ProjectManager"))
                    {
                        ModelState.AddModelError("Token_Error", "You are not allowed to access this function!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Token_Error", "There is no ClaimType.Role in this jwt token!");
                }
            }
            #endregion

            #region Call Odata API
            // Only run if there's no problem with jwt Token
            if (ModelState.ErrorCount == 0)
            {
                var apiUrl = $"https://localhost:7202/odata/CompanyProject/{id}";

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(apiUrl),
                    Method = HttpMethod.Delete,
                };

                //add jwt to authorization header
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                try
                {
                    //var response = await _httpClientFactory.CreateClient().GetAsync(apiUrl);
                    var client = _httpClientFactory.CreateClient();
                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<SimpleApiResonse>(responseContent);

                        if (apiResponse.Status)
                        {
                            // Success
                            return RedirectToPage("Management");
                        }
                        else
                        {
                            // Return API validator Error
                            foreach (var error in apiResponse.ErrorMessage)
                            {
                                ModelState.AddModelError("Odata_Error", error);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Odata_Error", "Invalid response from the API");
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Odata_Error", ex.Message);
                }
            }
            #endregion

            // Failed
            #region Call Odata API to set up CompanyProject
            // Only run if there's no problem with jwt Token
            if (ModelState.ErrorCount != 0)
            {
                // Only get CompanyProject
                var apiUrl = $"https://localhost:7202/odata/CompanyProject?$expand=participatingProjects($expand=employee($expand=department))&$filter=CompanyProjectID eq {id}";
                // Note: $count from OData count all the record INSIDE DB, not the records that had QUERIED up
                // Note 2: Exception with $filter query, only count number of records AFTER Filter

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(apiUrl),
                    Method = HttpMethod.Get,
                };

                //add jwt to authorization header
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

                try
                {
                    //var response = await _httpClientFactory.CreateClient().GetAsync(apiUrl);
                    var client = _httpClientFactory.CreateClient();
                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var odataObject = JsonConvert.DeserializeObject<OdataCompanyProjectModel>(responseContent);

                        if (odataObject != null)
                        {
                            if (odataObject.Value.Count != 0)
                            {
                                CompanyProject = odataObject.Value[0];// Get the Filtered Value
                            }
                            else
                            {
                                CompanyProject = new CompanyProjectModel(); // Temporarily set up a new CompanyProject for empty value and read error code
                                ModelState.AddModelError("Odata_Error", "Failed to get Company Project");
                            }

                        }
                        else
                        {
                            ModelState.AddModelError("Odata_Error", "Failed JsonConvert");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Odata_Error", "Invalid response from the API");
                    }

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Odata_Error", ex.Message);
                }
            }
            #endregion
            return Page();
        }
    }
}

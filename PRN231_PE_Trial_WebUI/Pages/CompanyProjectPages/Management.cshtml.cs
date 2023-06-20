using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PRN231_PE_Trial_WebUI.UIModels;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace PRN231_PE_Trial_WebUI.Pages.CompanyProjectPages
{
    public class ManagementModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ManagementModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IList<CompanyProjectModel> CompanyProjects { get; set; } = default!;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 1;
        public int TotalPages { get; set; }
        public PaginationNavBlockStatus PaginationStatus { get; set; }

        public enum PaginationNavBlockStatus
        {
            SearchAll,
            SearchProjectName,
            SearchEstStartDate
        }

        [BindProperty]
        public string? SearchProjectName { get; set; }
        [BindProperty]
        public DateTime? SearchEstStartDate { get; set; }


        public async Task<IActionResult> OnGet(int? pageNumber)
        {
            PaginationStatus = PaginationNavBlockStatus.SearchAll;

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
                // Update pageNumber if changed to next/previous page
                if (pageNumber.HasValue)
                {
                    PageNumber = pageNumber.Value;
                }

                // Only get CompanyProject
                var apiUrl = $"https://localhost:7202/odata/CompanyProject?$skip={(PageNumber - 1) * PageSize}&$top={PageSize}&$count=true";
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
                            CompanyProjects = odataObject.Value;
                            // Calculate TotalPages
                            TotalPages = (int)Math.Ceiling((double)odataObject.NumberOfRecords / PageSize);
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

        public async Task<IActionResult> OnPostSearchProjectName(int? pageNumber)
        {
            PaginationStatus = PaginationNavBlockStatus.SearchProjectName;

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

            if (SearchProjectName.IsNullOrEmpty())
            {
                ModelState.AddModelError("Odata_Error", "Search Project Name field can not be empty!");
            }

            #region Call Odata API
            // Only run if there's no problem with jwt Token
            if (ModelState.ErrorCount == 0)
            {
                // Update pageNumber if changed to next/previous page
                if (pageNumber.HasValue)
                {
                    PageNumber = pageNumber.Value;
                }

                // Only get CompanyProject
                var apiUrl = $"https://localhost:7202/odata/CompanyProject?$skip={(PageNumber - 1) * PageSize}&$top={PageSize}&$count=true&$filter=contains(ProjectName,'{SearchProjectName}')";
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
                            CompanyProjects = odataObject.Value;
                            // Calculate TotalPages
                            TotalPages = (int)Math.Ceiling((double)odataObject.NumberOfRecords / PageSize);
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

        public async Task<IActionResult> OnPostSearchEstStartDate(int? pageNumber)
        {
            PaginationStatus = PaginationNavBlockStatus.SearchEstStartDate;

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

            if (!SearchEstStartDate.HasValue)
            {
                ModelState.AddModelError("Odata_Error", "Search Estimate Start Date field can not be empty!");
            }

            #region Call Odata API
            // Only run if there's no problem with jwt Token
            if (ModelState.ErrorCount == 0)
            {
                // Update pageNumber if changed to next/previous page
                if (pageNumber.HasValue)
                {
                    PageNumber = pageNumber.Value;
                }

                var searchDate = SearchEstStartDate.Value.ToString("yyyy-MM-dd");

                // Only get CompanyProject
                var apiUrl = $"https://localhost:7202/odata/CompanyProject?$skip={(PageNumber - 1) * PageSize}&$top={PageSize}&$count=true&$filter=EstimatedStartDate eq {searchDate}";
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
                            CompanyProjects = odataObject.Value;
                            // Calculate TotalPages
                            TotalPages = (int)Math.Ceiling((double)odataObject.NumberOfRecords / PageSize);
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

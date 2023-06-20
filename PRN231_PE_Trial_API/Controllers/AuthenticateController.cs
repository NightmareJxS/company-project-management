using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using PRN231_PE_Trial_API.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PRN231_PE_Trial_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        private readonly HrStaffRepository _hrStaffRepository;
        private readonly string? _secretKey;

        public AuthenticateController(HrStaffRepository hrStaffRepository)
        {
            _hrStaffRepository = hrStaffRepository;

            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            _secretKey = config["SecretKey"];
        }

        /// <summary>
        /// Login to get token
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /HrStaff/Login
        ///     ---Admin---
        ///     {
        ///         "email": "a@a.a",
        ///         "password": "123"
        ///     }
        ///     ---ProjectManager---
        ///     {
        ///         "email": "c@c.c",
        ///         "password": "123"
        ///     }
        /// </remarks>
        /// <param name="credentials"></param>
        /// <returns>token</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] HrStaffViewModel credentials)
        {
            bool status = false;
            string errorMessage = string.Empty;
            var token = new TokenModel();

            try
            {
                var account = _hrStaffRepository.Get(x => x.Email == credentials.Email && x.Password == credentials.Password);

                if (account != null)
                {
                    status = true;

                    token = await GenerateToken(account);

                }
                else
                {
                    errorMessage = "Invalid credentials"; // or "You are not allowed to access this function!" according to the requirement
                }
            }
            catch (Exception ex)
            {
                status = false;
                errorMessage = ex.Message;
            }

            return new JsonResult(new
            {
                status = status,
                errorMessage = errorMessage,
                token = token
            });
        }


        #region Generate Token
        private async Task<TokenModel> GenerateToken(HRStaff account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            #region Claims
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, account.FullName),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, account.PropjectRole.ToString())
            };
            #endregion

            // Add handle permisson later

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: credentials,
                    expires: DateTime.Now.AddMinutes(30),
                    issuer: "CompanyX"
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = GenerateRefreshToken();// use for refresh token later (use it properly by store 1 instance in server/cache side)

            return new TokenModel()
            {
                JWTToken = jwt,
                RefreshToken = refreshToken
            };
        }


        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);

                return Convert.ToBase64String(random);
            }
        }

        #endregion

    }
}

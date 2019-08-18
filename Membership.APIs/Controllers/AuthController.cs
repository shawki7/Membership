using Membership.Dtos.Authentication;
using Membership.IData.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedMolecules.Core.OperationResult;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Membership.APIs.Controllers
{
    public class AuthController : Controller
    {
        #region Properties
        private const string TokenType = "Bearer";
        private  string JwtKey {get; set;}
        private string Issuer { get; set; }

        private const int TokenValidationDays = 30;
        public IConfiguration Configuration { get; set; }
        public IAuthService AuthService { get; set; }
        #endregion

        #region Constructors
        public AuthController(IConfiguration configuration, IAuthService accountService)
        {
            Configuration = configuration;
            AuthService = accountService;
            Issuer = Configuration["Jwt:Issuer"];
            JwtKey = Configuration["Jwt:Key"];
        }
        #endregion

        #region Methods

        #region Post

        [HttpPost]
        public IActionResult SignUp([FromBody] SignUpDto signUpDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }
            var registrationResult = AuthService.SignUp(signUpDto);
            if (registrationResult.Result == SMResult.Success)
            {
                return Json(ConfigureUserToken(registrationResult.Entity));
            }
            return Json("Failed");
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }
            var userInfo = AuthService.Login(loginDto).Entity;
            if (userInfo == null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Json("not found");
            }
            return Json(ConfigureUserToken(userInfo));
        }

        [HttpPost]
        public IActionResult IsEmailExists([FromBody]string email)
        {
            return Json(AuthService.IsEmailExists(email));
        }

        [HttpPost]
        public IActionResult IsUsernameExists([FromBody]string username)
        {
            return Json(AuthService.IsUsernameExists(username));
        }
        #endregion

        #region Helpers

        private AuthDto ConfigureUserToken(AuthDto user)
        {
            var token = new JwtSecurityToken(
               Issuer,
               Issuer,
              claims: GetUserClaims(user),
              expires: ReturnExpiresDate(),
              signingCredentials: getSigningCredentials()
             );

            user.ExpireDate = ReturnExpiresDate();
            user.Token = new JwtSecurityTokenHandler().WriteToken(token);
            user.TokenType = TokenType;
            return user;
        }

        private SigningCredentials getSigningCredentials() {
            return new
                SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey))
                                     ,SecurityAlgorithms.HmacSha256);
        }
        private DateTime ReturnExpiresDate()
        {
            return DateTime.UtcNow.AddDays(TokenValidationDays);
        }

        private IEnumerable<Claim> GetUserClaims(AuthDto user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
            return claims;
        }


        #endregion

        #endregion
    }
}

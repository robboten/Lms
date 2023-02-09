using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lms.Api.Controllers
{
    /// <summary>
    /// Controller for Authentication token
    /// </summary>
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        readonly IConfiguration _configuration;

        /// <summary>
        /// Request body for Authentication
        /// </summary>
        public class AuthenticationRequestBody
        {
            /// <summary>
            /// User name
            /// </summary>
            public string? UserName { get; set; }
            /// <summary>
            /// Password
            /// </summary>
            public string? Password { get; set; }
        }

        /// <summary>
        /// User info
        /// </summary>
        private class UserLoginInfo
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }

            public UserLoginInfo(int id, string userName, string password, string email)
            {
                Id = id;
                UserName = userName;
                Password = password;
                Email = email;
            }
        }
        /// <summary>
        /// Constuctor for auth controller
        /// </summary>
        /// <param name="configuration"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="authenticationRequestBody"></param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Security Token for user</response>
        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            var user = ValidateUserCredentials(authenticationRequestBody.UserName, authenticationRequestBody.Password);

            if (user == null)
            {
                return Unauthorized();
            }
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var claimsForToken = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("user_name", user.UserName.ToString()),
                new Claim("email", user.Email.ToString())
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
                );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(tokenToReturn);
        }

        private static UserLoginInfo ValidateUserCredentials(string? userName, string? password)
        {
            return new UserLoginInfo(1, userName ?? "", "bobby", "ba@ck.es");
        }
    }
}

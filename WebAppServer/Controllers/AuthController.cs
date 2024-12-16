using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAppServer.DbContexts;
using WebAppServer.Models.DTO;
using WebAppServer.Models.Users;
using WebAppServer.Utils;

namespace WebAppServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private ApplicationContext _context;

        public AuthController()
        {
            _context = new ApplicationContext();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] RegisterRequestDto registerRequest)
        {
            var identity = GetIdentity(registerRequest.Login, registerRequest.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid login or password!" });
            }
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSynmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                accesToken = encodedJwt,
                username = identity.Name,
                role = identity.Claims.FirstOrDefault(c => c.Type.Contains("role"))?.Value
            };

            return Ok(JsonConvert.SerializeObject(response));
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto registerRequest)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == registerRequest.Login);
            if (user != null)
            {
                return BadRequest("User with this login already exist");
            }
            _context.Users.Add(new User
            {
                Login = registerRequest.Login,
                Password = AuthUtils.HashPassword(registerRequest.Password),
                Role = "users"
            });
            _context.SaveChanges();
            return Ok();
        }

        private ClaimsIdentity GetIdentity(string login, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == login);
            if (user == null || !AuthUtils.VerifyPassword(password, user.Password))
            {
                return null;
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
                };

            var claimsIdentity
                = new ClaimsIdentity(claims, "Token",
                        ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

    }
}

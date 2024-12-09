using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAppServer.DbContexts;
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

        private List<User> _users = new List<User>
        {
            new User { Login = "admin@mail.ru", Password = "12345", Role = "admin"},
            new User { Login = "user@mail.ru", Password="12345", Role = "user"},
        };

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            var identity = GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password!" });
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
                acces_token = encodedJwt,
                username = identity.Name
            };

            return Ok(JsonConvert.SerializeObject(response));
        }

        [HttpPost("register")]
        public IActionResult Register(string username, string password)
        {
            _context.Users.Add(new User
            {
                Login = username,
                Password = Utils.AuthUtils.HashPassword(password),
                Role = "user"
            });
            var id = _context.SaveChanges();
            return Ok(id);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == username);
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

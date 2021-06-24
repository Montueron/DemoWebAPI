using DataAccess;
using DataAccess.Context;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly DataContext _dc;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IConfiguration config, DataContext dc, ILogger<LoginController> logger)
        {
            _config = config;
            _dc = dc;
            _logger = logger;
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] User login)
        {
            _logger.LogInformation("Called Login method info");
            _logger.LogWarning("Called Login method warning");
            _logger.LogDebug("Called Login method debug");
            _logger.LogError("Called Login method error");
            _logger.LogTrace("Called Login method trace");
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        //Todo replace User with DTO
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim("UserRole", userInfo.Role));

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims.ToArray(),
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials
              );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //Todo replace User with DTO
        private User AuthenticateUser(User login)
        {
            User user = UserHelper.GetUserByUserName(_dc, login.UserName);
            return user;
        }
    }
}

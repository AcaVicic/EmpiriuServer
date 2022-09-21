using Domain;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration configuration;
        readonly EmpiriuContext context;

        public TokenController(IConfiguration config, EmpiriuContext context)
        {
            configuration = config;
            this.context = context;
        }

        [HttpPost("User")]
        public async Task<IActionResult> Post(User u)
        {
            if (u != null && u.Email != null && u.Password != null)
            {
                var user = GetUser(u.Email, u.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim("Email", user.Email!)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        claims : claims,
                        expires: DateTime.UtcNow.AddMinutes(180),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private User? GetUser(string email, string password)
        {
            return context.Users!.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}

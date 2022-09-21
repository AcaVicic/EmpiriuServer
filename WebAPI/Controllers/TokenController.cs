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
    /// <summary>
    /// This class represents controller which generates JSON Web Token for users of Empiriu application.
    /// </summary>
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        /// <summary>
        /// <value>This property represents the instance of a set key/value application configuration properties.</value>
        /// </summary>
        public IConfiguration configuration;
        /// <summary>
        /// <value> 
        /// Property <c>context</c> represents the instance which communicates with Empiriu database.
        /// </value>
        /// </summary>
        readonly EmpiriuContext context;

        /// <summary>
        /// This costructor initializes the context and config to passed parameters.
        /// </summary>
        /// <param name="context">the new instance of context</param>
        /// <param name="config">the applications configuration</param>
        public TokenController(IConfiguration config, EmpiriuContext context)
        {
            configuration = config;
            this.context = context;
        }

        /// <summary>
        /// This method generates JSON Web Token for the given user and returns adequate response.
        /// </summary>
        /// <param name="u">the user that logged in</param>
        /// <returns>Returns JSON Web Token if the user exists with Ok respons, or Bad request if user doesn't exists.</returns>
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

        /// <summary> 
        /// This method returns user registered with given credentials.
        /// </summary>
        /// <param name="email">email of the user that wants to log in</param>
        /// <param name="password">password of the user that wants to log in</param>
        /// <returns>User registered with given credentials.</returns>
        private User? GetUser(string email, string password)
        {
            return context.Users!.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}

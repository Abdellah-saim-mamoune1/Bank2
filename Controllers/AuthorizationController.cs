using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.ClientXEmployeeDto_s;
using bankApI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace bankApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AuthorizationController(AppDbContext context)
        {
            _db = context;
        }


        [HttpGet("check-auth")]
        public IActionResult CheckAuth()
        {
            var jwt = Request.Cookies["jwt"];
            Console.WriteLine("JWT in cookies: " + jwt);
            if (string.IsNullOrEmpty(jwt)) return Unauthorized();

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes("thisIsAReallyStrongSecretKey1234567890");

                tokenHandler.ValidateToken(jwt, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return Ok(new { message = "Authorized" });
            }
            catch
            {
                return Unauthorized();
            }
        }




        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(DSignIn login)

        {
            string Type = "";
            bool IsAccountFrozen = false;

            if (login.Email.Contains("@Nova.com"))
            {
                var employeeInfos = await _db.EmployeeAccount.FirstOrDefaultAsync(p => p.Account == login.Email
                &&p.Password==login.Password);
                if (employeeInfos == null)
                {
                    return Unauthorized(new { message = "Invalid credentials" });
                }


                Type = "Employee";
                IsAccountFrozen = employeeInfos.IsFrozen;
            }
            else
            {
                var client = await _db.Accounts
                    .FirstOrDefaultAsync(e => e.AccountAddress == login.Email && e.PassWord == login.Password);

                if (client == null)
                {
                    return Unauthorized(new { message = "Invalid credentials" });
                }
                Type = "Client";
                IsAccountFrozen = client.IsFrozen;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("thisIsAReallyStrongSecretKey1234567890");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
        new Claim(ClaimTypes.Email, login.Email)

    }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = "yourIssuer",
                Audience = "yourAudience",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);




            return Ok(new { token = jwt,type=Type,frozen=IsAccountFrozen });
        }




     
    }
}

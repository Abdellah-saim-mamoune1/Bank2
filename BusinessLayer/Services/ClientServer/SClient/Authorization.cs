using Azure;
using bankApI.BusinessLayer.Dto_s;
using bankApI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bankApI.BusinessLayer.Services.ClientServer.SClient
{
   /* public class Authorization
    {

        private readonly AppDbContext _db;

       public Authorization (AppDbContext db)
        {
            _db = db;
        }

        public async Task<DGetEmployee> SignInAsync(DSignIn signIn)
        {
          var email =await _db.EmployeeAccount.FirstOrDefaultAsync(c=>c.Account == signIn.Email);
            if (email==null|| email.Password != signIn.Password)
            {
                return null;
            }

           else
            {
                var claims = new[]
           {
                new Claim(ClaimTypes.Name, login.Username)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                // Set JWT as HttpOnly cookie
                Response.Cookies.Append("jwt", jwt, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });

               
            }


        }
    }*/
}

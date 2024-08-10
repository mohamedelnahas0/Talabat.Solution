using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Services;
using System.IdentityModel.Tokens.Jwt;

namespace Talabat.Service
{
    public class TokenService : ITokenServices
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
           _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser User, UserManager<AppUser> userManager)
        {
            //payload
            //private claims
            var Authclaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName , User.DisplayName),
                new Claim(ClaimTypes.Email , User.Email),

            };

            var UserRoles = await userManager.GetRolesAsync(User);
            foreach (var Role in UserRoles)  
            {
                Authclaims.Add(new Claim(ClaimTypes.Role, Role));
            }
            var Authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var Token = new JwtSecurityToken(

            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires:DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
            claims: Authclaims ,signingCredentials: 
            new SigningCredentials(Authkey,SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}

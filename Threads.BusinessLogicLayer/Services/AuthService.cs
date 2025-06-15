

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Threads.BusinessLogicLayer.DTO.RegisterDTO;
using Threads.BusinessLogicLayer.Models;
using Threads.BusinessLogicLayer.ServiceContracts;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly JWT _jwt;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JWT> jwt)
        {
            _jwt = jwt.Value;
            _userManager = userManager;
        }

        public async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            List<Claim> roleClaims = roles
                .Select(x => new Claim("roles", x)).ToList();

            var claims = new[]
            {
                //subject user id
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),

                //jwt unique id
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                //Date and Time of Token Genration
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                //Unique Name Identifier of the user (email)
                new Claim(ClaimTypes.NameIdentifier, user.Email!),
                //Name  of the user (PersonName)
                new Claim(ClaimTypes.Name, user.PersonName!),
            }.Union(userClaims).Union(roleClaims);


            SymmetricSecurityKey securityKey = new
               SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));

            SigningCredentials signingCredentials =
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials
            );

            return jwtSecurityToken;
        }
    }
}

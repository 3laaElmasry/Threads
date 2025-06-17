

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Threads.BusinessLogicLayer.DTO.RegisterDTO;
using Threads.BusinessLogicLayer.Models;
using Threads.BusinessLogicLayer.ServiceContracts;
using Threads.DataAccessLayer;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly JWT _jwt;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AuthService(IOptions<JWT> jwt,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _jwt = jwt.Value;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<AuthModel> RegisterAsync(Register user)
        {
            if (await _userManager.FindByEmailAsync(user.Email) is not null)
            {
                return new AuthModel { Message = "Email is Already Exist", IsAuthonticated = false };
            }

            if (await _userManager.FindByNameAsync(user.UserName) is not null)
            {
                return new AuthModel { Message = "User Name is Already Exist", IsAuthonticated = false };
            }

            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                PersonName = user.PersonName,

            };

            var result = await _userManager.CreateAsync(applicationUser, user.Password);

            if (!result.Succeeded)
            {
                var errorMessage = String.Join(",", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = errorMessage, IsAuthonticated = false };

            }

            await _userManager.AddToRoleAsync(applicationUser, Statics.User_Role);
            await _signInManager.SignInAsync(applicationUser, false);

            var jwtSecurityToken = await CreateJwtToken(applicationUser);


            var roles = await _userManager.GetRolesAsync(applicationUser);

            var authResponse = new AuthModel
            {
                Email = applicationUser.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthonticated = true,
                Roles = roles.ToList(),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = applicationUser.UserName,
                Message = "Succeded"
            };

            return authResponse;

        }

        public async Task<AuthModel> GetJwtTokenAsync(UserLoginModel userLogin)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(userLogin.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, userLogin.Password))
            {
                authModel.Message = "Email address or password is incorrect!";
                authModel.IsAuthonticated = false;
                return authModel;
            }

            await _signInManager.SignInAsync(user, false);
            var jwtSecurityToken = await CreateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);


            authModel.Email = user.Email!;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.IsAuthonticated = true;
            authModel.Roles = roles.ToList();
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.UserName = user.UserName!;
            authModel.Message = "Succeded";

            return authModel;
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

               new Claim(JwtRegisteredClaimNames.Iat,
               new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
               ClaimValueTypes.Integer64),

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


using System.IdentityModel.Tokens.Jwt;
using Threads.BusinessLogicLayer.DTO.RegisterDTO;
using Threads.BusinessLogicLayer.Models;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.ServiceContracts
{
    public interface IAuthService
    {
        Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);

        Task<AuthModel> RegisterAsync(Register user);

        Task<AuthModel> GetJwtTokenAsync(UserLoginModel user);
    }
}

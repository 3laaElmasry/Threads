
using Threads.BusinessLogicLayer.DTO.RegisterDTO;
using Threads.BusinessLogicLayer.Models;

namespace Threads.BusinessLogicLayer.ServiceContracts
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAysnc(Register model);
    }
}

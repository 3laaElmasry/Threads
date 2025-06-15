
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.DTO.RegisterDTO
{
    public static class ReigsterExtenstions
    {
        public static RegisterResponse ToRegisterResponse (this ApplicationUser user)
        {
            return new RegisterResponse
            {
                Id = user.Id.ToString(),
                UserName = user.UserName ?? "",
                Email = user.Email ?? "",
                ImageUrl = user.ImageUrl,
                PersonName = user.PersonName??"",

            };
        }
    }

}

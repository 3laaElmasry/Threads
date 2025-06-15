
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.DTO.RegisterDTO
{
    public static class UserExtenstions
    {
        public static UserResponse ToRegisterResponse (this ApplicationUser user)
        {
            return new UserResponse
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

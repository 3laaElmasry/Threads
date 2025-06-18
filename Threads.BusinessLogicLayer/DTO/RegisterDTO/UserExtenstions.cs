
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.DTO.RegisterDTO
{
    public static class UserExtenstions
    {
        public static UserResponse ToRegisterResponse (this UserProfile user)
        {
            return new UserResponse
            {
                UserId = user.ClerkUserId.ToString(),
                DisplayName = user.DisplayName ?? "",
                ImgUrl = user.ImgUrl,

            };
        }
    }

}

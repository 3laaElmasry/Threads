
using Threads.DataAccessLayer.Data.Entities;

using Clerk.BackendAPI.Models.Components;
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

        public static UserProfile ToUserProfile(this User user)
        {
            return new UserProfile
            {
                ClerkUserId = user.Id!,
                DisplayName = user.Username,
                ImgUrl = user.ImageUrl,
            };
        }
    }

}

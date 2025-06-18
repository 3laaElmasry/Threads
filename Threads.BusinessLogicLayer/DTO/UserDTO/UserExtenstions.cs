
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
                UserId = user.UserId.ToString(),
                DisplayName = user.DisplayName ?? "",
                ImgUrl = user.ImgUrl,

            };
        }

        public static UserProfile ToUserProfile(this User user)
        {
            var userEmail = user.EmailAddresses?.FirstOrDefault()?.EmailAddressValue;
            return new UserProfile
            {
                UserId = user.Id!,
                DisplayName = user.Username,
                ImgUrl = user.ImageUrl,
                Email = userEmail
            };
        }
    }

}


using Clerk.BackendAPI;
using Clerk.BackendAPI.Models.Components;
using Threads.BusinessLogicLayer.DTO.RegisterDTO;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.Services
{
    public class ClerkUserService : IClerkUserService
    {
        private readonly ClerkBackendApi _clerk;

        public ClerkUserService(ClerkBackendApi clerk)
        {
            _clerk = clerk;
        }

        async Task<User?> IClerkUserService.GetUserAsync(string clerkUserId)
        {
            var res = await _clerk.Users.GetAsync(clerkUserId);
            if (res is null)
                return null;
            return res.User;
        }

         async Task<List<UserProfile>> IClerkUserService.GetUsersByUsernameAsync(string username)
        {
            var usersResponse = await _clerk.Users.ListAsync();

            var clerkUsers = usersResponse.UserList;

            if (clerkUsers is null)
            {
                return new List<UserProfile>();
            }


            return  clerkUsers.Where(u => u.Username == username).Select(u => u.ToUserProfile()).ToList();
        }

    }

}


using Clerk.BackendAPI;
using Clerk.BackendAPI.Models.Components;

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
    }

}

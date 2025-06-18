using Clerk.BackendAPI;
using Clerk.BackendAPI.Models;
using Clerk.BackendAPI.Models.Components;
public interface IClerkUserService
{
    Task<User?> GetUserAsync(string clerkUserId);
}

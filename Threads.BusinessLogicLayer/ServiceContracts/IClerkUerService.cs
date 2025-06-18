using Clerk.BackendAPI;
using Clerk.BackendAPI.Models;
using Clerk.BackendAPI.Models.Components;
using Threads.DataAccessLayer.Data.Entities;
public interface IClerkUserService
{
    Task<User?> GetUserAsync(string clerkUserId);
    Task<List<UserProfile>> GetUsersByUsernameAsync(string username);
}

using Clerk.BackendAPI.Models.Components;

using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.ServiceContracts
{
    public interface IUserProfileService
    {
        Task<UserProfile?> Add(User user);
        Task<UserProfile?> GetUser(string userProfileId);
    }
}

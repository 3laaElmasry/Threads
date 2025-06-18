
using Clerk.BackendAPI.Models.Components;
using Threads.BusinessLogicLayer.DTO.RegisterDTO;
using Threads.BusinessLogicLayer.ServiceContracts;
using Threads.DataAccessLayer.Data.Entities;
using Threads.DataAccessLayer.RepositoryContracts;

namespace Threads.BusinessLogicLayer.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public async Task<UserProfile?> Add(User user)
        {
            var newUserProfile = user.ToUserProfile();
            await _userProfileRepository.AddAsync(newUserProfile);
            await _userProfileRepository.Save();
            return newUserProfile;
        }

        public async Task<UserProfile?> GetUser(string userProfileId)
        {
            var userFromDb = await _userProfileRepository
                .GetAsync(u => u.UserId == userProfileId);
            return userFromDb;
        }

        public async Task<bool> IsExist(string userProfileId)
        {
            return await GetUser(userProfileId) != null;
        }
    }
}

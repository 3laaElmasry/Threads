

using Threads.DataAccessLayer.Data;
using Threads.DataAccessLayer.Data.Entities;
using Threads.DataAccessLayer.RepositoryContracts;

namespace Threads.DataAccessLayer.Repository
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}

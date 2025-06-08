

using Threads.DataAccessLayer.Data;
using Threads.DataAccessLayer.Data1.Entities;
using Threads.DataAccessLayer.RepositoryContracts;

namespace Threads.DataAccessLayer.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}

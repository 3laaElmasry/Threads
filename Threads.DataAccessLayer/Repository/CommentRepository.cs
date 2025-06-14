

using Threads.DataAccessLayer.Data;
using Threads.DataAccessLayer.Data.Entities;
using Threads.DataAccessLayer.RepositoryContracts;

namespace Threads.DataAccessLayer.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}

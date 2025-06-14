
using Threads.BusinessLogicLayer.DTO.CommentDTO;
using Threads.BusinessLogicLayer.ServiceContracts;
using Threads.DataAccessLayer.RepositoryContracts;

namespace Threads.BusinessLogicLayer.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<List<CommentResponse>?> GetAll(string postId)
        {
            var commentsFromDb = await _commentRepository
                .GetAllAsync(c => c.PostId.ToString() == postId && c.ParentId == null,includeProperties:"Author");

            var commentResponses = commentsFromDb.Select(c => c.ToCommentResponse()).ToList();
            return commentResponses;
        }
    }
}

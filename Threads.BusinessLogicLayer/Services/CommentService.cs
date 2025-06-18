
using Threads.BusinessLogicLayer.DTO.CommentDTO;
using Threads.BusinessLogicLayer.ServiceContracts;
using Threads.DataAccessLayer.Data.Entities;
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

        public async Task<CommentResponse?> CreateComment(CommentRequest commentRequest)
        {
            if(!string.IsNullOrEmpty(commentRequest.ParentId))
            {
                var parentComment = await _commentRepository
                    .GetAsync(c => c.CommentId.ToString() == commentRequest.ParentId);
                if(parentComment != null)
                {
                    parentComment.Replys += 1;
                    _commentRepository.Update(parentComment);
                }
            }

            Comment newComment = commentRequest.ToComment();
            await _commentRepository.AddAsync(newComment);
            await _commentRepository.Save();
            return newComment.ToCommentResponse();

        }

        public async Task<List<CommentResponse>?> GetAll(string postId)
        {
            var commentsFromDb = await _commentRepository
                .GetAllAsync(c => c.TweetId.ToString() == postId && c.ParentId == null,includeProperties:"Author");

            var commentResponses = commentsFromDb.Select(c => c.ToCommentResponse()).ToList();
            return commentResponses;
        }

        public async Task<CommentResponse?> GetCommentById(string commentId)
        {
            var commentFromDb = await _commentRepository
                .GetAsync(c => c.CommentId.ToString() == commentId,includeProperties:"Author");
            if (commentFromDb is null)
                return null;
            return commentFromDb.ToCommentResponse();
        }

        public async Task<List<CommentResponse>> GetCommentReplies(string parentId)
        {
            var childsFromDb = await _commentRepository
                .GetAllAsync(c => c.ParentId.ToString() == parentId,includeProperties:"Author");

            var childsResponses = childsFromDb.Select(c => c.ToCommentResponse()).ToList();
            return childsResponses;
        }
    }
}

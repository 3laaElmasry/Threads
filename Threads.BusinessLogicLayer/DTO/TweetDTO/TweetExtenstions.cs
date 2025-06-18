using Threads.BusinessLogicLayer.DTO.CommentDTO;
using Threads.BusinessLogicLayer.DTO.PostDTO;
using Threads.BusinessLogicLayer.DTO.RegisterDTO;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.DTO.PostExtenstions
{
    public static class TweetExtenstions
    {
        public static Tweet ToPost(this TweetRequest postRegisterDTO)
        {
            return new Tweet()
            {
                Text = postRegisterDTO.Text,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                AuthorId = Guid.Parse(postRegisterDTO.AuthorId),
                TweetId = Guid.NewGuid(),
            };
        }
       

        public static TweetResponse ToPostResponse(this Tweet postRegisterDTO)
        {
            return new TweetResponse()
            {
                Text = postRegisterDTO.Text,
                CreatedDate = postRegisterDTO.CreatedDate,
                UpdatedDate = postRegisterDTO.UpdatedDate,
                AuthorId = postRegisterDTO.AuthorId,
                PostId = postRegisterDTO.TweetId.ToString(),
                Comments = postRegisterDTO.Comments?.Select(c => c.ToCommentResponse()).ToList(),
                Author = postRegisterDTO.Author?.ToRegisterResponse(),

            };
        }
    }
}

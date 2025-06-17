

using Threads.BusinessLogicLayer.DTO.RegisterDTO;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.DTO.CommentDTO
{
    public static class CommentExtesntions
    {
        public static Comment ToComment(this CommentRequest comment)
        {
            return new Comment
            {
                CommentId = Guid.NewGuid(),
                AuthorId = Guid.Parse(comment.AuthorId!),
                PostId = Guid.Parse(comment.PostId!),
                ParentId = String.IsNullOrEmpty(comment.ParentId)? null : Guid.Parse(comment.ParentId),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Text = comment.Text!,
            };
        }


        public static CommentResponse ToCommentResponse(this Comment comment)
        {
            return new CommentResponse
            {
                Author = comment.Author?.ToRegisterResponse(),
                AuthorId = comment.AuthorId.ToString(),
                CommentId = comment.CommentId.ToString(),
                PostId = comment.PostId.ToString(),
                ParentId = comment.ParentId.ToString(),
                CreatedDate = comment.CreatedDate,
                UpdatedDate = comment.UpdatedDate,
                Text = comment.Text,
                Replys = comment.Replys
            };
        }
    }
}

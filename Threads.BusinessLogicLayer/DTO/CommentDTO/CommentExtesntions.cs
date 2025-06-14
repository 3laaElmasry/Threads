

using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.DTO.CommentDTO
{
    public static class CommentExtesntions
    {
        public static Comment ToComment(this CommentRequest comment)
        {
            Guid.TryParse(comment.ParentId,out var paraentId);
            return new Comment
            {
                CommentId = Guid.NewGuid(),
                AuthorId = Guid.Parse(comment.AuthorId!),
                PostId = Guid.Parse(comment.PostId!),
                ParentId = paraentId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Text = comment.Text!,
            };
        }
    }
}

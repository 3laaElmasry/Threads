using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Threads.DataAccessLayer.Data.Entities
{

    public class Comment
    {
        public Guid CommentId { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public string AuthorId { get; set; } = string.Empty;

        public Guid TweetId { get; set; }

        public Guid? ParentId { get; set; }

        public int Replys { get; set; } = 0;
        public UserProfile? Author { get; set; }
        public Tweet? Tweet { get; set; }
        public Comment? Parent { get; set; }


        public List<Comment> Replies { get; set; } = new List<Comment>();

    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Threads.DataAccessLayer.Data1.Entities
{

    public class Comment
    {
        public Guid CommentId { get; set; }

        [Required]
        public string CommentText { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [Required]
        public String UserId { get; set; } = String.Empty;

        [ForeignKey("UserId")]
        public User? User { get; set; }


        [Required]
        public String PostId { get; set; } = String.Empty;

        [ForeignKey("PostId")]
        public Post? Post { get; set; }



        public String ParentId { get; set; } = String.Empty;
        [ForeignKey("ParentId")]
        public Comment? Parent { get; set; }

        [ValidateNever]
        public IEnumerable<Comment>? Children { get; set; }

    }
}

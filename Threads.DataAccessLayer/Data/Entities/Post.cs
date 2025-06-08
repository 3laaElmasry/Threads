using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Threads.DataAccessLayer.Data1.Entities
{
    public class Post
    {
        public Guid PostId { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [Required]
        public String UserId { get; set; } = String.Empty;

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ValidateNever]
        public IEnumerable<Comment>? Comments { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace Threads.DataAccessLayer.Data1.Entities
{
    public class Post
    {
        public Guid PostId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Guid AuthorId { get; set; }
        public ApplicationUser? Author { get; set; }

        [ValidateNever]
        public List<Comment>? Comments { get; set; }
    }
}

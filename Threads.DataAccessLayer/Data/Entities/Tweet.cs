using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace Threads.DataAccessLayer.Data.Entities
{
    public class Tweet
    {
        public Guid TweetId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public string AuthorId { get; set; } = string.Empty;
        public UserProfile? Author { get; set; }

        [ValidateNever]
        public List<Comment>? Comments { get; set; }
    }
}

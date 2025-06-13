

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.DTO.PostDTO
{
    public class PostResponse
    {
        public string PostId { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Guid AuthorId { get; set; }
        public ApplicationUser? Author { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}

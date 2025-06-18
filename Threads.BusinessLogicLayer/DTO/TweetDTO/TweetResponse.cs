

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Threads.BusinessLogicLayer.DTO.CommentDTO;
using Threads.BusinessLogicLayer.DTO.RegisterDTO;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.DTO.PostDTO
{
    public class TweetResponse
    {
        public string PostId { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public string AuthorId { get; set; } = string.Empty;
        public UserResponse? Author { get; set; }
        public List<CommentResponse>? Comments { get; set; }
    }
}

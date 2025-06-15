
using Threads.BusinessLogicLayer.DTO.RegisterDTO;

namespace Threads.BusinessLogicLayer.DTO.CommentDTO
{
    public class CommentResponse
    {
        public string? CommentId { get; set; }

        public string? Text { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public string? AuthorId { get; set; }

        public string? PostId { get; set; }

        public string? ParentId { get; set; }

        public int Replys { get; set; }

        public UserResponse? Author { get; set; }
    }
}

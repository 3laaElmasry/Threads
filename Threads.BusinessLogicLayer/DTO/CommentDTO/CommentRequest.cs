

using System.ComponentModel.DataAnnotations;

namespace Threads.BusinessLogicLayer.DTO.CommentDTO
{
    public class CommentRequest
    {
        [Required(ErrorMessage = "The Comment cann't be null or empty")]
        public string? Text { get; set; }

        [Required(ErrorMessage = "Post Id is Required to Publish a Comment")]
        public string? PostId { get; set; }

        public string? ParentId { get; set; }
        public string? AuthorId { get; set; }

    }
}

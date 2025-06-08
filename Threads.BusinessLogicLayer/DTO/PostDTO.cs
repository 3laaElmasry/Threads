

using System.ComponentModel.DataAnnotations;
using Threads.DataAccessLayer.Data1.Entities;

namespace Threads.BusinessLogicLayer.DTO
{
    public class PostDTO
    {

        [Required(ErrorMessage = "Post Description cann't be null")]
        public string Text { get; set; } = string.Empty;


        public Guid PostId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Guid AuthorId { get; set; }

    }
}

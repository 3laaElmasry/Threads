using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.DTO.PostExtenstions
{
    public class PostRequest
    {

        [Required(ErrorMessage = "Post Description cann't be null")]
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Guid AuthorId { get; set; }

    }
}

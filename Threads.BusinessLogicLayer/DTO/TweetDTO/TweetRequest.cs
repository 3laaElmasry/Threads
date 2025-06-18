using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.BusinessLogicLayer.DTO.PostExtenstions
{
    public class TweetRequest
    {

        [Required(ErrorMessage = "Post Description cann't be null")]
        public string Text { get; set; } = string.Empty;

        public string AuthorId { get; set; } = string.Empty;

    }
}

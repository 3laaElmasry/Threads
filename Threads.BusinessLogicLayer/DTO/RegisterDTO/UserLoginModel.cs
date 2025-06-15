

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Threads.BusinessLogicLayer.DTO.RegisterDTO
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "The Email cann't be blank")]
        [EmailAddress(ErrorMessage = "The Email must be valid with email constraints")]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}



using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Threads.BusinessLogicLayer.DTO.RegisterDTO
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "The Email cann't be blank")]
        [EmailAddress(ErrorMessage = "The Email must be valid with email constraints")]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "The Password can't be blank")]
        [RegularExpression("^(?=.*[a-z]).{5,}$",
    ErrorMessage = "Password must be at least 5 characters and include at least one lowercase letter.")]

        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}

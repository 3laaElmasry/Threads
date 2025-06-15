
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Threads.DataAccessLayer.Data.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? ImageUrl { get; set; }

        [Required]
        [Range(maximum: 50,minimum:3)]
        public string? PersonName { get; set; }
    }
}

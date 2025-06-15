
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Threads.DataAccessLayer.Data.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? ImageUrl { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? PersonName { get; set; }
    }
}

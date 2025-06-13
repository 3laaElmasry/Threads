
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Threads.DataAccessLayer.Data.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? ImageUrl { get; set; }
    }
}

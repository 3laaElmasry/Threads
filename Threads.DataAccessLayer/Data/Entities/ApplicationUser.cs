
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Threads.DataAccessLayer.Data1.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {

        [ValidateNever]
        public List<Post>? Posts { get; set; }

        [ValidateNever]
        public List<Comment>? Comments { get; set; }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Threads.DataAccessLayer.Data.Entities;
using Threads.DataAccessLayer.Data1.Entities;

namespace Threads.DataAccessLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

      

    }
}

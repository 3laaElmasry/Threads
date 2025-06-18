using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Threads.DataAccessLayer.Data.Configurations;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.DataAccessLayer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Tweet> Tweets { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<UserProfile> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TweetEntityConfiguraion).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}

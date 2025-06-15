using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.DataAccessLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Post entity
            modelBuilder.Entity<Post>()
                .HasKey(p => p.PostId);

            modelBuilder.Entity<Post>()
                .Property(p => p.Text)
                .IsRequired()
                .HasMaxLength(1000);

            modelBuilder.Entity<Post>()
                .Property(p => p.CreatedDate)
                .IsRequired();

            // Define relationship: Post belongs to an Author (User)
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithMany()
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Cascade); // Prevent cascade delete from User to Post


            // Configure Comment entity
            modelBuilder.Entity<Comment>()
                .HasKey(c => c.CommentId);

            modelBuilder.Entity<Comment>()
                .Property(c => c.Text)
                .IsRequired()
                .HasMaxLength(1000);

            modelBuilder.Entity<Comment>()
                .Property(c => c.CreatedDate)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(c => c.UpdatedDate);

           

            // Define relationship: Comment belongs to a Post
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction); // Ensure cascade delete

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Parent)
                .WithMany()
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .Property(c => c.Replys);

            base.OnModelCreating(modelBuilder);
        }

    }
}

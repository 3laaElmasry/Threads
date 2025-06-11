using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Threads.DataAccessLayer.Data.Entities;
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
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascade delete from User to Post

            // Define relationship: Post has many Comments
            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascade delete from Post to Comment

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

            // Define relationship: Comment belongs to an Author (User)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascade delete from User to Comment

            // Define relationship: Comment belongs to a Post
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.NoAction); // Ensure no cascade delete

            base.OnModelCreating(modelBuilder);
        }

    }
}

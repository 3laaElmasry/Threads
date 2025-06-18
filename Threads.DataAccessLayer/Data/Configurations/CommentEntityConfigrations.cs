

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.DataAccessLayer.Data.Configurations
{
    public class CommentEntityConfigrations : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> modelBuilder)
        {
            // Configure Comment entity
            modelBuilder
            .HasKey(c => c.CommentId);

            modelBuilder
                .Property(c => c.Text)
                .IsRequired()
                .HasMaxLength(1000);

            modelBuilder
                .Property(c => c.CreatedDate)
                .IsRequired();

            modelBuilder
                .Property(c => c.UpdatedDate);



            // Define relationship: Comment belongs to a Tweet
            modelBuilder
                .HasOne(c => c.Tweet)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.TweetId)
                .OnDelete(DeleteBehavior.NoAction); // Ensure cascade delete

            modelBuilder
                .HasOne(c => c.Parent)
                .WithMany(p => p.Replies)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.HasOne(c => c.Author)
                .WithMany()
                .HasForeignKey(c => c.AuthorId)
                .IsRequired();

            modelBuilder
                .Property(c => c.Replys);
        }
    }
}

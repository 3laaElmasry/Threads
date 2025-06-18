

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.DataAccessLayer.Data.Configurations
{
    public class TweetEntityConfiguraion : IEntityTypeConfiguration<Tweet>
    {
        public void Configure(EntityTypeBuilder<Tweet> modelBuilder)
        {
            // Configure the Post entity
            modelBuilder.HasKey(t => t.TweetId);

            modelBuilder.Property(p => p.Text)
                .IsRequired()
                .HasMaxLength(1000);

            modelBuilder
                .Property(p => p.CreatedDate)
                .IsRequired();

            // Define relationship: Post belongs to an Author (User)
            modelBuilder
                .HasOne(p => p.Author)
                .WithMany(u => u.Tweets)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}

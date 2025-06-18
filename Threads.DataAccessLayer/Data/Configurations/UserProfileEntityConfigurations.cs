
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Threads.DataAccessLayer.Data.Entities;

namespace Threads.DataAccessLayer.Data.Configurations
{
    public class UserProfileEntityConfigurations : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(u => u.ClerkUserId);
            builder.Property(u => u.DisplayName)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(u => u.ImgUrl)
                .HasMaxLength(200)
                .IsRequired(false);

        }
    }
}

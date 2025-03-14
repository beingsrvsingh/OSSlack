using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Data.EntityConfigurations
{
    internal class AspNetUserInfoEntityTypeConfiguration : IEntityTypeConfiguration<AspNetUserInfo>
    {
        public void Configure(EntityTypeBuilder<AspNetUserInfo> entity)
        {
            entity.HasIndex(x => x.UserId).IsUnique();
            entity.Property(x => x.FirstName).HasColumnType("nvarchar(100)");
            entity.Property(x => x.LastName).HasColumnType("nvarchar(100)");
            entity.Property(x => x.AvatarURI).HasColumnType("nvarchar(500)");
            entity.Property(x => x.IsMembership).HasColumnType("bit");
            entity.Property(x => x.MembershipId).HasColumnType("nvarchar(256)");
        }
    }
}

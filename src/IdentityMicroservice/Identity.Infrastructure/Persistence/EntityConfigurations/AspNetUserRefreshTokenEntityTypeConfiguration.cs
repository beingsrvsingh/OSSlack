using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Data.EntityConfigurations
{
    internal class AspNetUserRefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<AspNetUserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<AspNetUserRefreshToken> entity)
        {
            entity.HasKey(x => new { x.UserId, x.Token});
            //entity.HasIndex(x => x.UserId).IsClustered(true).IsUnique();
            //entity.Property(x => x.Token).HasColumnType("nvarchar(450)");
            //entity.Property(x => x.LastName).HasColumnType("nvarchar(50)");
            //entity.Property(x => x.AvatarURI).HasColumnType("nvarchar(500)");
        }
    }
}

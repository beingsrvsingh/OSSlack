using JwtTokenAuthentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtTokenAuthentication.Infrastructure.Persistence.EntityConfigurations
{
    internal class AspNetUserSecurityTokenEntityTypeConfiguration : IEntityTypeConfiguration<AspNetUserSecurityToken>
    {
        public void Configure(EntityTypeBuilder<AspNetUserSecurityToken> entity)
        {
            entity.HasIndex(e => new { e.UserId }).IsClustered(false);
            entity.Property(e => e.UserId).HasMaxLength(100).IsRequired();
            entity.Property(e => e.SecurityKey).HasMaxLength(350).IsRequired();
        }
    }
}

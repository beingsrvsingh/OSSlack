using JwtTokenAuthentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtTokenAuthentication.Infrastructure.Persistence.EntityConfigurations
{
    internal class AspNetUserSecurityTokenEntityTypeConfiguration : IEntityTypeConfiguration<AspNetUserSigningKey>
    {
        public void Configure(EntityTypeBuilder<AspNetUserSigningKey> entity)
        {
            entity.ToTable("aspnet_user_security_tokens");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id");

            entity.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            entity.Property(x => x.EncryptedSigningKey)
                .HasColumnName("encrypted_signing_key")
                .HasMaxLength(512);

            entity.Property(x => x.SigningHash)
                .HasColumnName("signing_hash")
                .HasMaxLength(200);

            entity.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .IsRequired();

            entity.Property(x => x.ExpiresAt)
                .HasColumnName("expires_at");

            entity.Property(x => x.LastUsedAt)
                .HasColumnName("last_used_at");

            entity.Property(x => x.IsRevoked)
                .HasColumnName("is_revoked");

            entity.Property(x => x.CreatedByIp)
                .HasColumnName("created_by_ip")
                .HasMaxLength(45);

            entity.Property(x => x.UserAgent)
                .HasColumnName("user_agent")
                .HasMaxLength(256);

            entity.HasOne(m => m.User)
                     .WithOne(u => u.AspNetUserSigningKey)
                     .HasForeignKey<AspNetUserSigningKey>(m => m.UserId);
        }
    }
}

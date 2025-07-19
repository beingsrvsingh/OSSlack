using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Data.EntityConfigurations
{
    internal class AspNetUserRefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<AspNetUserRefreshToken>
    {
            public void Configure(EntityTypeBuilder<AspNetUserRefreshToken> entity)
            {
                  // Table name
                  entity.ToTable("aspnet_user_refresh_token");

                  // Primary key
                  entity.HasKey(e => e.Id);

                  // Properties
                  entity.Property(e => e.Id)
                        .HasColumnName("id")
                        .ValueGeneratedOnAdd();

                  entity.Property(e => e.UserId)
                        .IsRequired()
                        .HasMaxLength(450) // Match AspNetUser Id size
                        .HasColumnName("user_id");

                  entity.Property(e => e.Token)
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnName("token");

                  entity.Property(e => e.Expires)
                        .IsRequired()
                        .HasColumnName("expires");

                  entity.Property(e => e.Created)
                        .IsRequired()
                        .HasColumnName("created");

                  entity.Property(e => e.CreatedByIp)
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnName("created_by_ip");

                  entity.Property(e => e.Revoked)
                        .HasColumnName("revoked");

                  entity.Property(e => e.RevokedByIp)
                        .HasMaxLength(45)
                        .HasColumnName("revoked_by_ip");

                  entity.Property(e => e.ReplacedByToken)
                        .HasMaxLength(512)
                        .HasColumnName("replaced_by_token");

                  entity.HasOne(e => e.User)
                        .WithMany(e => e.AspNetUserRefreshTokens)
                        .HasForeignKey(e => e.UserId);
        }
    }

}

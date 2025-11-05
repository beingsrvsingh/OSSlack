using Astrologer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astrologer.Infrastructure.Persistence.EntityConfigurations
{
    public class AstrologerAddonConfiguration : IEntityTypeConfiguration<AstrologerAddon>
    {
        public void Configure(EntityTypeBuilder<AstrologerAddon> builder)
        {
            // Table name
            builder.ToTable("astrologer_addon");

            // Primary Key
            builder.HasKey(p => p.Id)
                   .HasName("pk_astrologer_addon_id");

            // Properties with snake_case
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
            builder.Property(p => p.Description).HasColumnName("description").HasMaxLength(300);
            builder.Property(p => p.Price).HasColumnName("price").HasColumnType("decimal(12,2)");
            builder.Property(p => p.Currency).HasColumnName("currency").HasMaxLength(50).HasDefaultValue("INR");
            builder.Property(p => p.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            builder.Property(p => p.DisplayOrder).HasColumnName("display_order").HasDefaultValue(0);
            builder.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            // Foreign key
            builder.Property(p => p.AstrologerId).HasColumnName("astrologer_id");
            builder.Property(p => p.AstrologerExpertiseId).HasColumnName("astrologer_expertise_id");

            builder.HasOne(p => p.Astrologer)
                   .WithMany(p => p.AstrologerAddons)
                   .HasForeignKey(p => p.AstrologerId)
                   .HasConstraintName("fk_astrologer_addon_astrologer_id")
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.AstrologerExpertise)
                   .WithMany(p => p.AstrologerAddons)
                   .HasForeignKey(p => p.AstrologerExpertiseId)
                   .HasConstraintName("fk_astrologer_addon_astrologer_expertise_id")
                   .OnDelete(DeleteBehavior.SetNull);

        }
    }
}

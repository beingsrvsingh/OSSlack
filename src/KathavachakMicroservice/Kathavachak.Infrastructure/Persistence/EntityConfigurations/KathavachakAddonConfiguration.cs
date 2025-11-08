using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Kathavachak.Domain.Entities;

namespace Kathavachak.Infrastructure.Persistence.EntityConfigurations
{
    public class KathavachakAddonConfiguration : IEntityTypeConfiguration<KathavachakAddon>
    {
        public void Configure(EntityTypeBuilder<KathavachakAddon> builder)
        {
            // Table name
            builder.ToTable("kathavachak_addon");

            // Primary Key
            builder.HasKey(p => p.Id)
                   .HasName("pk_kathavachak_addon_id");

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
            builder.Property(p => p.KathavachakId).HasColumnName("kathavachak_id");
            builder.Property(p => p.KathavachakExpertiseId).HasColumnName("kathavachak_expertise_id");

            builder.HasOne(p => p.KathavachakMaster)
                   .WithMany(p => p.KathavachakAddons)
                   .HasForeignKey(p => p.KathavachakId)
                   .HasConstraintName("fk_kathavachak_addon_kathavachak_id")
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.KathavachakExpertise)
                   .WithMany(p => p.KathavachakAddons)
                   .HasForeignKey(p => p.KathavachakExpertiseId)
                   .HasConstraintName("fk_kathavachak_addon_kathavachak_variant_id")
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

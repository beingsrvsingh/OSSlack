using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Priest.Domain.Entities;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class PriestAddonConfiguration : IEntityTypeConfiguration<Addon>
    {
        public void Configure(EntityTypeBuilder<Addon> builder)
        {
            // Table name
            builder.ToTable("priest_addon");

            // Primary Key
            builder.HasKey(p => p.Id)
                   .HasName("pk_priest_addon_id");

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
            builder.Property(p => p.PriestId).HasColumnName("priest_id");
            builder.Property(p => p.PriestExpertiseId).HasColumnName("priest_expertise_id");

            builder.HasOne(p => p.PriestMaster)
                   .WithMany(p => p.Addons)
                   .HasForeignKey(p => p.PriestId)
                   .HasConstraintName("fk_priest_addon_priest_id")
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.PriestExpertise)
                   .WithMany(p => p.Addons)
                   .HasForeignKey(p => p.PriestExpertiseId)
                   .HasConstraintName("fk_priest_addon_priest_expertise_id")
                   .OnDelete(DeleteBehavior.SetNull);

        }
    }
}

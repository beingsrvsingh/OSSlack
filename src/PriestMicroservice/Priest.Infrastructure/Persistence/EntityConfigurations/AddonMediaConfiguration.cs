using Priest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Priest.Infrastructure.Persistence.EntityConfigurations
{
    public class AddonMediaConfiguration : IEntityTypeConfiguration<AddOnMedia>
    {
        public void Configure(EntityTypeBuilder<AddOnMedia> entity)
        {
            entity.ToTable("add_on_media");

            entity.HasKey(img => img.Id);

            entity.Property(img => img.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(img => img.ImageUrl)
                .HasColumnName("image_url")
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(entity => entity.SortOrder)
                .HasColumnName("sort_order");

            entity.Property(entity => entity.AltText)
                .HasColumnName("alt_text");

            entity.Property(img => img.AddonId)
                .HasColumnName("add_on_id")
                .IsRequired();

            entity.Property(vi => vi.MediaType)
                .HasColumnName("media_type")
                .HasConversion<string>();

            entity.Property(p => p.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            entity.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            entity.Property(p => p.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasOne(img => img.Addon)
                .WithMany(p => p.AddOnMedias)
                .HasForeignKey(img => img.AddonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

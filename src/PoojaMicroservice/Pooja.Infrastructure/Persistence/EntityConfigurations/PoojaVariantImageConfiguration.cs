using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pooja.Domain.Entities;

namespace Pooja.Infrastructure.Persistence.EntityConfigurations
{
    public class PoojaVariantImageConfiguration : IEntityTypeConfiguration<PoojaVariantImage>
    {
        public void Configure(EntityTypeBuilder<PoojaVariantImage> builder)
        {
            builder.ToTable("pooja_variant_image");

            builder.HasKey(vi => vi.Id);

            builder.Property(vi => vi.Id)
                .HasColumnName("id");

            builder.Property(vi => vi.ImageUrl)
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnName("image_url");

            builder.Property(vi => vi.SortOrder)
                .HasColumnName("sort_order");

            builder.Property(vi => vi.MediaType)
                .HasColumnName("media_type")
                .HasConversion<string>();

            builder.Property(vi => vi.AltText)
                .HasMaxLength(50)
                .HasColumnName("alt_text");

            builder.Property(vi => vi.PoojaVariantId)
                .IsRequired()
                .HasColumnName("pooja_variant_id");

            builder.Property(p => p.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            builder.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.HasOne(vi => vi.PoojaVariantMaster)
                .WithMany(v => v.Medias)
                .HasForeignKey(vi => vi.PoojaVariantId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

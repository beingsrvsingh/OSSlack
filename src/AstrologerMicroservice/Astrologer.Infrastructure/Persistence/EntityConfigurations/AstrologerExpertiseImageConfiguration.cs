using Astrologer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Astrologer.Infrastructure.Persistence.EntityConfigurations
{
    public class AstrologerExpertiseImageConfiguration : IEntityTypeConfiguration<AstrologerExpertiesMedia>
    {
        public void Configure(EntityTypeBuilder<AstrologerExpertiesMedia> builder)
        {
            builder.ToTable("astrologer_expertise_image");

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

            builder.Property(vi => vi.AstrolgerExpertiesId)
                .IsRequired()
                .HasColumnName("astrologer_expertise_id");

            builder.Property(p => p.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            builder.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            builder.HasOne(vi => vi.AstrologerExpertise)
                .WithMany(v => v.AstrologerExpertiseMedia)
                .HasForeignKey(vi => vi.AstrolgerExpertiesId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
